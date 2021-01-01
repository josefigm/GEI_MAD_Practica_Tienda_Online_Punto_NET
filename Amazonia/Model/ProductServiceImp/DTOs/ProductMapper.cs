
namespace Es.Udc.DotNet.Amazonia.Model.ProductServiceImp.DTOs
{
    public class ProductMapper
    {
        public static ProductDTO ProductToProductDto(Product input)
        {
            return new ProductDTO(input.id, input.name, input.price, input.entryDate, input.Category);
        }

        public static CompleteProductDTO ProductToCompleteProductDto(Product input)
        {
            return new CompleteProductDTO(input.id, input.name, input.price, input.entryDate, input.Category.name, input.Category.id, input.image, input.description, input.stock);
        }
    }
}
