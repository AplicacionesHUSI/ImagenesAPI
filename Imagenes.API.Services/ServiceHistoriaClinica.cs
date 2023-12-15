using Imagenes.API.Domain.DbSet;
using Imagenes.API.Domain.IRepository;
using Imagenes.API.Domain.IService;
using Imagenes.API.Domain.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Imagenes.API.Services
{
    public class ServiceHistoriaClinica : IServiceHistoriaClinica
    {
        private readonly IRepositoryElemento _repo;

        private readonly string _pathFileServer;

        public ServiceHistoriaClinica(IRepositoryElemento repository, IConfiguration configuration) {
            _repo = repository;
            _pathFileServer = configuration["pathFileServer"];
        }

        public async  Task<Elemento> FindElemento(int IdElemento)
        {
            return await _repo.FindElemento(IdElemento);
        }

        public async Task<int> SaveElement(HistoriaClinicaRequest request)
        {
            var res = request.Elemento;
            /* var res = string.Empty;
             if (request.tipoElemento == "video" || request.tipoElemento == "foto" || request.tipoElemento == "imagen")
                 res = base64ToFile(request.Elemento, request.tipoElemento,request.IdElemento);
             else 
             if (string.IsNullOrEmpty(res)) return 0;
             */
            var aux = await _repo.FindGrupo(request.IdRegistro);
            if (aux == null)
            {
                aux = new ElementosGrupo()
                {
                    FecCreacion = DateTime.Now,
                    IdRegistro = request.IdRegistro,
                    IdAtencion = request.idAtencion,
                    IdMedico = request.IdMedico
                };
                if (request.tipoElemento == "asunto")
                    aux.Asunto = res;
                if (request.tipoElemento == "observaciones")
                    aux.Observaciones = res;
                aux.IdGrupo = await _repo.SaveGrupo(aux);
            }
            else {
                if (request.tipoElemento == "asunto")
                    aux.Asunto = res;
                if (request.tipoElemento == "observaciones")
                    aux.Observaciones = res;
                await _repo.UpdateGrupo(aux);
            }
            if (request.tipoElemento == "video" || request.tipoElemento == "imagen" || request.tipoElemento == "foto")
            {
                res=res.Replace(_pathFileServer, "");
                var elemento = new Elemento()
                {
                    IdElemento = request.IdElemento,
                    IdGrupo = aux.IdGrupo,
                    FecRegistro = DateTime.Now,
                    Tipo = request.tipoElemento,
                    Element = res,
                    NombreElemento = request.nombreElemento

                };
                await _repo.SaveElemento(elemento);
            }
           return  request.IdElemento;
        }

        private string base64ToFile(string data, string tipo,int IdElemento)
        {

            var extension = string.Empty;
            switch (tipo) {
                case "foto":
                    extension = "jpg";
                    break;
                case "imagen":
                    extension = "jpg";
                    break;
                case "video":
                    extension = "mp4";
                    break;
            }
            try
            {
                byte[] ret = Convert.FromBase64String(data);
                
                var currentDate = DateTime.Now;
                var folder = $"\\{currentDate.Year}\\{currentDate.Month}";
                var pathFileServer = _pathFileServer + folder;
                if (!Directory.Exists(pathFileServer))
                {
                    Directory.CreateDirectory(pathFileServer);
                }
                folder+=$"\\{ IdElemento}.{ extension}";                
                var path = $"{_pathFileServer}{folder}";
                FileInfo fil = new FileInfo(path);

                using (Stream sw = fil.OpenWrite())
                {
                    sw.Write(ret, 0, ret.Length);
                    sw.Close();
                }
                return path;
            }       
            catch (Exception e) {
                return null;
            }            
        }
    }
}
