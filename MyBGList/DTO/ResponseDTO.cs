namespace MyBGList.DTO
{
    public class ResponseDTO<T>
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();

        public T Data { get; set; } = default!;
    }
}
