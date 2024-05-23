namespace BooksManager.Infraestructure.Extensions
{
    public class EmailExtensions
    {
        public static string EmailTemplate(string nomeDoUsuario, string tituloDoLivro, string dataDeDevolucao)
        {
            var templatePath = @"E:\Caio\repos\BooksManager\BooksManager.Infraestructure\Templates\EmailTemplate.html";

            var templateContent = File.ReadAllText(templatePath);

            var emailContent = PopulateTemplate(templateContent, nomeDoUsuario, tituloDoLivro, dataDeDevolucao);

            return emailContent;
        }

        public static string PopulateTemplate(string template, string nomeDoUsuario, string tituloDoLivro, string dataDeDevolucao)
        {
            return template
                .Replace("{{NomeDoUsuario}}", nomeDoUsuario)
                .Replace("{{TituloDoLivro}}", tituloDoLivro)
                .Replace("{{DataDeDevolucao}}", dataDeDevolucao);
        }
    }
}
