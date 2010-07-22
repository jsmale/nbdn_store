namespace nothinbutdotnetstore.model
{
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
		  public int? parentId { get; set; }
    }
}