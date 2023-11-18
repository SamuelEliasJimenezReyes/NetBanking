namespace NetBanking.Core.Application.ViewModel.AvancedeEfectivo
{
    public class SaveAvanceDeEfectivo
    {
        public string IdUser {  get; set; }
        public string CardNumber {  get; set; }
        public string DestinationAccount {  get; set; }
        public decimal Amount {  get; set; }

        public bool HasError {  get; set; }
        public string Error {  get; set; }
    }
}
