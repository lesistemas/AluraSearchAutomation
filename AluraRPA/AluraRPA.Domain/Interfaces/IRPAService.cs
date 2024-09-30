namespace AluraRPA.Dominio
{
    // Define a interface para o serviço de automação, abstraindo a lógica de busca
    public interface IRPAService
    {
        void ExecutarBusca(string termoDeBusca);
    }
}
