namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa =  Console.ReadLine();
            if (ValidarPlaca(placa))
            {
                if (!VeiculoEstacionado(placa))
                {
                    veiculos.Add(placa);
                    Console.WriteLine($"O veículo {placa} foi estacionado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Este veículo já está estacionado aqui. Cancelando operação.");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida. O modelo correto é: ABC-1234");
            }
        }

        public void RemoverVeiculo()
        {
            if (!veiculos.Any())
            {
                Console.WriteLine("Não há veículos estacionados.");
                return;
            }
            string placa = ObterPlacaValida();
            if (VeiculoEstacionado(placa))
            {
                int horas = ObterInteiro("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                decimal valorTotal = precoInicial + precoPorHora * horas;
                DeletarVeiculo(placa);
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                ExibirVeiculosLista();
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private static int ObterInteiro(string msg)
        {
            Console.WriteLine(msg);
            if(int.TryParse(Console.ReadLine(), out int inteiro))
            {
                return inteiro;
            }
            Console.WriteLine("Valor inválido.");
            return ObterInteiro(msg);
        }

        private static bool ValidarPlaca(string placa)
        {   
            placa = placa.Replace("-", "");
            if(placa.Length == 7)
            {
                if(placa[..3].All(char.IsLetter) && placa.Substring(3, 4).All(char.IsDigit))
                {
                    return true;
                }
            }
            return false;
        }

        private static string PlacaNormalizada(string placa)
        {
            return placa.Replace("-", "").ToUpper();
        }

        private void ExibirVeiculosLista()
        {
            foreach (var veiculo in veiculos)
            {
                Console.WriteLine(veiculo);
            }
        }

        private void ExibirVeiculosComIndice()
        {
            for (int i = 0; i < veiculos.Count; i++)
            {
                Console.Write($"{i + 1} - {veiculos[i]}");
                if(i < veiculos.Count - 1)
                {
                    Console.Write(" | ");
                }

            }
            Console.WriteLine();
        }

        private bool VeiculoEstacionado(string placa)
        {
            return veiculos.Any(x => PlacaNormalizada(x) == PlacaNormalizada(placa));
        }

        private string ObterPlacaValida()
        {
            ExibirVeiculosComIndice();
            Console.WriteLine("Digite a placa do veículo ou ID para remover:");
            string placa = Console.ReadLine();
            if(ValidarPlaca(placa))
            {
                return placa;
            }
            if(int.TryParse(placa, out int id))
                {
                    if(id > 0 && id <= veiculos.Count)
                    {
                        return veiculos[id - 1];
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                        return ObterPlacaValida();
                    }
                }
            Console.WriteLine("Placa inválida. O modelo correto é: ABC-1234");
            return ObterPlacaValida();
        }

        private bool DeletarVeiculo(string placa)
        {
            placa = PlacaNormalizada(placa);
            for (int i = 0; i < veiculos.Count; i++)
            {
                if(PlacaNormalizada(veiculos[i]) == placa)
                {
                    veiculos.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

    }
}
