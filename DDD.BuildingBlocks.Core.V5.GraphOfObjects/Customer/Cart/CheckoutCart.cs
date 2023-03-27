using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Exceptions;
using DDD.BuildingBlocks.Core.V5.GraphOfObjects.Shared;

namespace DDD.BuildingBlocks.Core.V5.GraphOfObjects.Customer.Cart;

public sealed class CheckoutCart
{
    private List<CartProduct> _products = new();

    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Payment Payment { get; private set; }
    public Shipment Shipment { get; private set; }
    public IReadOnlyCollection<CartProduct> Products => _products;

    internal CheckoutCart(Cart cart, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CustomerId = cart.CustomerId;
        _products = cart.Products.ToList();
    }

    internal void SetShipment(Shipment shipment)
        => Shipment = shipment;

    internal void SetPayment(Payment payment)
    {
        var cartValue = _products.Sum(x => x.Quantity * x.Product.Price);

        // does this logic belong to the Cart itself?
        if (cartValue > 20_000 && payment.PaymentMethod is PaymentMethod.Cash)
        {
            throw new PaymentMethodNotAllowedException();
        }

        Payment = payment;
    }

    internal Order.Order PlaceOrder()
    {
        return Order.Order.CreateFromCheckout(this);
    }
}