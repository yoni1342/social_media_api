namespace Galacticos.Application.DTOs.Relations{
    public class GetFollowersDTO{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
    }
}