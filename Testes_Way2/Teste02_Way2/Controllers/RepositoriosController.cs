using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Octokit;
using System.Threading.Tasks;
using Teste02_Way2.Models;


namespace Teste02_Way2.Controllers
{
    public class RepositoriosController : Controller
    {
        //Client ID:60cd02621d499deaab84
        //Client Secret:6d9c6e769ba08355e829a67e9b2c4a81c79ce645
        GitHubClient Client;

        public void Credenciais()
        {
            Client = new GitHubClient(new ProductHeaderValue("Teste02Way2"), new Uri("https://github.com/"));
            Client.Connection.Credentials = new Credentials("6a0600b7c1c840d96f41465643d08aaea47f4fd4");
        }

        public async Task<ActionResult> MeusRepositorios()
        {
            Credenciais();
            return View((await Client.Repository.GetAllForCurrent() as IEnumerable<Repository>).ToList());
        }

        public async Task<ActionResult> BuscarRepositorios(string nomeRepositorio)
        {
            Credenciais();
            if (string.IsNullOrEmpty(nomeRepositorio)) nomeRepositorio = "*";

            return View((await Client.Search.SearchRepo(new SearchRepositoriesRequest(nomeRepositorio))).Items.ToList());
        }

        public async Task<ActionResult> RepositoriosFavoritos()
        {
            Credenciais();
            return View((await new StarredClient(new ApiConnection(Client.Connection)).GetAllForCurrent()).ToList());
        }

        public async Task<ActionResult> DetalhesRepositorio(string usuario, string repositorioNome)
        {
            Credenciais();
            var repositorio = await Client.Repository.Get(usuario, repositorioNome);
            List<Octokit.User> contribuidores = null;
            try
            {
                contribuidores = (List<Octokit.User>) await Client.Repository.RepoCollaborators.GetAll(usuario, repositorioNome);
            }
            catch(Exception ex)
            {
            }
            var favoritos = await new StarredClient(new ApiConnection(Client.Connection)).GetAllForCurrent();
            var heFavorito = favoritos.Any(x => x.Id == repositorio.Id);

            var detalhesRepositorio = new DetalhesRepositorioModel
            {
                Contribuidores = contribuidores == null ? null : contribuidores.ToList(),
                Repositorio = repositorio,
                HeFavorito = heFavorito
            };
            return View(detalhesRepositorio);
        }


        public async Task<ActionResult> Favorito(string usuario, string repositorioNome, bool favorito)
        {
            Credenciais();
            if (favorito)
                await Client.Connection.Delete(ApiUrls.Starred(usuario, repositorioNome));
            else
                await Client.Connection.Put<object>(ApiUrls.Starred(usuario, repositorioNome), null, null);

            return RedirectToAction("DetalhesRepositorio", new { usuario, repositorioNome });
        }
    


        //private async Task Login()
        //{
        //    _Client.Connection.Credentials = new Credentials(Username, Password);
  
        //    var newAuthorization = new NewAuthorization
        //    {
        //        Scopes = new List<string> { "user", "repo", "delete_repo", "notifications", "gist" },
        //        Note = "Grimpoteuthis"
        //    };
 
        //    try
        //    {
        //    var authorization = await _Client.Authorization.GetOrCreateApplicationAuthentication(
        //        "client-id-of-your-registered-github-application",
        //        "client-secret-of-your-registered-github-application",
        //        newAuthorization);
 
        //    _Client.Connection.Credentials = new Credentials(authorization.Token);
        //    RxApp.MutableResolver.Register(() => _Client, typeof(IGitHubClient));
      
        //    // Just to show that authentication was successful
        //    Username = String.Empty;
        //    Password = String.Empty;
        //    ErrorMessage = "Successfully authenticated with username password";
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //    }
        //}
    }
}
