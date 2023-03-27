using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer.Cart;
using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer.Order;
using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Exceptions;
using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Shared;

namespace DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer;

//NOTE: This is an example on how to not design an aggregate!
public class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private string _fullName;
    private Cart.Cart _activeCart;
    private CheckoutCart _activeCheckout;
    private readonly List<Order.Order> _orders = new();

    public Customer(string fullName, Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(fullName));
        _fullName = fullName;
        Id = id ?? Guid.NewGuid();
    }
    
    public void CreateCart()
    {
        var cart = new Cart.Cart(Id);
        _activeCart = cart;
    }

    public void AddProductToActiveCart(Product product)
    {
        EnsureActiveCart();
        _activeCart.AddProduct(product);
    }

    public void RemoveProductFromActiveCart(Product product)
    {
        EnsureActiveCart();
        _activeCart.RemoveProduct(product);
    }

    public void ClearActiveCart()
    {
        EnsureActiveCart();
        _activeCart.Clear();
    }

    private void EnsureActiveCart()
    {
        if (_activeCart is null)
        {
            throw new InvalidOperationException("There is no active cart for this customer now!");
        }
    }

    public void CheckoutActiveCart()
    {
        var checkout = _activeCart.Checkout();
        //TODO: Perhaps some logic to check if this particular cart has not been already checked out etc.
        _activeCheckout = checkout;
        _activeCart = null;
    }

    public void SetShipmentForActiveCheckout(Shipment shipment)
    {
        EnsureActiveCheckout();
        _activeCheckout.SetShipment(shipment);
    }

    public void SetPaymentForActiveCheckout(Payment payment)
    {
        EnsureActiveCheckout();
        _activeCheckout.SetPayment(payment);
    }
    
    private void EnsureActiveCheckout()
    {
        if (_activeCheckout is null)
        {
            throw new InvalidOperationException("There is no active checkout for this customer now!");
        }
    }

    public void PlaceOrderFromActiveCheckout()
    {
        var checkout = _activeCheckout;
        
        var limitedAvailabilityProductsInCheckout =
            checkout.Products.Where(x => x.Product.LimitedAvailability);

        var thisQuarter = new YearQuarter(DateTime.UtcNow);
        var thisQuarterStartDate = thisQuarter.GetQuarterStartDate();

        foreach (var limitedProduct in limitedAvailabilityProductsInCheckout)
        {
            if (limitedProduct.Quantity > 1)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }

            //DISCUSS: Perhaps place order date and finalize order date should be treated differently (edge case)
            var nonCanceledOrdersThisQuarter = _orders
                .Where(x => x.Status is not OrderStatus.Canceled)
                .Where(x => x.PlaceDateUtc >= thisQuarterStartDate);

            var productAlreadyPurchasedThisQuarter = nonCanceledOrdersThisQuarter.SelectMany(x => x.Lines)
                //TODO: Do we introduce SKU on OrderLine? Or match products with lines in some other way?
                .Any(x => x.Name.Equals(limitedProduct.Product.Name));

            if (productAlreadyPurchasedThisQuarter)
            {
                throw new ProductCountLimitExceededInGivenTimeInterval();
            }
        }

        var order = checkout.PlaceOrder();
        _orders.Add(order);
        _activeCheckout = null;
    }

    public void CompleteOrder(Guid orderId)
    {
        var order = GetCustomerOrderById(orderId);
        order.Complete();
    }

    public void CancelOrder(Guid orderId)
    {
        var order = GetCustomerOrderById(orderId);
        order.Cancel();
    }
    
    private Order.Order GetCustomerOrderById(Guid orderId)
    {
        var order = _orders.Find(x => x.Id.Equals(orderId));
        if (order is null)
        {
            throw new InvalidOperationException($"Could not find order of given id ({orderId}) for this customer");
        }
        
        return order;
    }
}