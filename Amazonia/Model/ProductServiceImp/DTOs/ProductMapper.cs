
namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs
{
    public class ProductMapper
    {
        public static ProductDTO ProductToProductDto(Product input)
        {
            return new ProductDTO(input.id, input.name, input.price, input.entryDate, input.Category);
        }
    }
}
