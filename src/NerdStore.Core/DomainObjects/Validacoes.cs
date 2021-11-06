using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NerdStore.Core.DomainObjects
{
    public class Validacoes
    {
        public static void ValidarSeIgual(object objct1, object object2,string message)
        {
            if (!objct1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeDiferente(object object1, object object2,string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarCaracteres(string valor, int maximo,string message)
        {
            var lenght = valor.Trim().Length;
            if(lenght > maximo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarCaracteres(string valor, int minimo, int maximo, string message)
        {
            var lenght = valor.Trim().Length;
            if(lenght < minimo || lenght > maximo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarExpressao(string pattern, string valor,string message)
        {
            var regex = new Regex(pattern);
            if (!regex.IsMatch(valor))
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeVazio(string valor, string message)
        {
            if(valor is null || valor.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeNulo(object objct1,string message)
        {
            if(objct1 is null)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarMinimoMaximo(double valor, double minimo,double maximo,string message)
        {
            if(valor < minimo || valor > maximo)
            {
                throw new Exception(message);
            }
        }
        public static void ValidarMinimoMaximo(int valor, int minimo, int maximo,string message)
        {
            if(valor < minimo || valor > maximo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string message)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarMinimoMaximo(float valor, float minimo,float maximo,string message)
        {
            if(valor <minimo || valor > maximo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarMinimoMaximo(long valor, long minimo, long maximo,string message)
        {
            if(valor < minimo || valor > maximo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeMenorIgualMinimo(int valor, int minimo,string message)
        {
            if(valor <= minimo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeMenorIgualMinimo(decimal valor, decimal minimo, string message)
        {
            if (valor <= minimo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeMenorIgualMinimo(float valor, float minimo, string message)
        {
            if (valor <= minimo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeMenorIgualMinimo(long valor, long minimo, string message)
        {
            if (valor <= minimo)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeFalso(bool boolvalor, string message)
        {
            if (boolvalor)
            {
                throw new DomainException(message);
            }
        }
        public static void ValidarSeVerdadeiro(bool boolvalor,string message)
        {
            if (!boolvalor)
            {
                throw new DomainException(message);
            }
        }
    }
}
