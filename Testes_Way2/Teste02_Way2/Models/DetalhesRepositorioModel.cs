using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Octokit;

namespace Teste02_Way2.Models
{
    public class DetalhesRepositorioModel
    {
        public Repository Repositorio { get; set; }
        public List<User> Contribuidores { get; set; }
        public bool HeFavorito { get; set; }
    }
}