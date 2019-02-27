using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Biblioteca.VOs;

namespace Biblioteca.DAOs
{
    public static class ImagemDAO
    {
        private static SqlParameter[] CriaParametrosDaImagem(ImagensVO imagens)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            parameters[0] = new SqlParameter("@CPF", imagens.Paciente.CPF);
            parameters[1] = new SqlParameter("@caminho", imagens.Caminho);
            

            return parameters;
        }

        public static void Insert(ImagensVO imagens)
        {
            string sql = "insert into Imagens(CPF,Caminho)" +
                           "values (@CPF,@caminho)";
            MetodosGenericosDeDAO.ExecutaSQL(sql, CriaParametrosDaImagem(imagens));
        }        
        public static void Delete(ImagensVO imagens)
        {
            
            string sql = "delete Imagens where (CPF = @CPF and Caminho = @caminho)";
            MetodosGenericosDeDAO.ExecutaSQL(sql, CriaParametrosDaImagem(imagens));
        }
        
    }
}
