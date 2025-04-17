namespace dopeClothes.Server.Models.VMs
{
    public class MyOrderVM
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
    }
}
