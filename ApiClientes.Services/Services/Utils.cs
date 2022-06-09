namespace ApiClientes.Services.Services
{
    public class Utils
    {
        public static int CalcularIdade(DateTime DataNascimento)
        {
            var idade = 0;
            if (DataNascimento < DateTime.Now)
            {
                idade = DateTime.Now.Year - DataNascimento.Year;
                if ((DateTime.Now.DayOfYear) < (DataNascimento.DayOfYear))
                    idade = idade - 1;
                
            }
            return idade;
        }
    }
}
