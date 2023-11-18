using NetBanking.Core.Application.ViewModel.AvancedeEfectivo;

namespace NetBanking.Core.Application.Interfaces.Services
{
    public interface IAvancedeEfectivo
    {
        Task<SaveAvanceDeEfectivo> MakeAvance(SaveAvanceDeEfectivo model);
    }
}
