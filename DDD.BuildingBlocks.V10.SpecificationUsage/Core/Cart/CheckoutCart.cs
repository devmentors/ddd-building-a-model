using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart.Specifications;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Exceptions;
using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Shared;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;

public sealed class CheckoutCart
{
    private List<CartProduct> _products = new();

    public Guid Id { get; }
    public Guid CustomerId { get; private set; }
    public Payment Payment { get; private set; }
    public Shipment Shipment { get; private set; }
    public IReadOnlyCollection<CartProduct> Products => _products;

    private CheckoutCart()
    {
    }
    
    internal CheckoutCart(Cart cart, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CustomerId = cart.CustomerId;
        _products = cart.Products.ToList();
    }

    public void SetShipment(Shipment shipment)
        => Shipment = shipment;

    public void SetPayment(Payment payment)
    {
        var cartValue = _products.Sum(x => x.Quantity * x.Product.Price);
        var canPayWithCash = new CanPayWithCash(cartValue).Check(payment);
        
        // does this logic belong to the Cart itself?
        if (canPayWithCash is false)
        {
            throw new PaymentMethodNotAllowedException();
        }

        Payment = payment;
    }

    public Order.Order PlaceOrder()
    {
        return Order.Order.CreateFromCheckout(this);
    }
}