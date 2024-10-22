namespace OnlineStore.Core.Entities.Order
{
    public class ProductItemOrder
    {
        public ProductItemOrder() { }

        public ProductItemOrder(string productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}