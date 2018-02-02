using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Negocio
{
    public class Imagen
    {
        //FUNCION PARA CONVERTIR LA IMAGEN A BYTES
        public Byte[] Imagen_A_Bytes(String ruta)

        {

            FileStream foto = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            Byte[] arreglo = new Byte[foto.Length];

            BinaryReader reader = new BinaryReader(foto);

            arreglo = reader.ReadBytes(Convert.ToInt32(foto.Length));

            return arreglo;

        }

        //FUNCION PARA CONVERTIR DE BYTES A IMAGEN
        public Bitmap Bytes_A_Imagen(Byte[] ImgBytes)

        {

            Bitmap imagen = null;

            Byte[] bytes = (Byte[])(ImgBytes);

            MemoryStream ms = new MemoryStream(bytes);

            imagen = new Bitmap(ms);

            return imagen;

        }

        //METODO PARA REDIMENSIONAR LA IMAGEN

        public String Redimensionar(Image Imagen_Original, string nombre)

        {

            //RUTA DEL DIRECTORIO TEMPORAL

            String DirTemp = Path.GetTempPath() + @"\" + nombre + ".jpg";

            //IMAGEN ORIGINAL A REDIMENSIONAR

            Bitmap imagen = new Bitmap(Imagen_Original);

            //CREAMOS UN MAPA DE BIT CON LAS DIMENSIONES QUE QUEREMOS PARA LA NUEVA IMAGEN

            Bitmap nuevaImagen = new Bitmap(Imagen_Original.Width, Imagen_Original.Height);

            //CREAMOS UN NUEVO GRAFICO

            Graphics gr = Graphics.FromImage(nuevaImagen);

            //DIBUJAMOS LA NUEVA IMAGEN

            gr.DrawImage(imagen, 0, 0, nuevaImagen.Width, nuevaImagen.Height);

            //LIBERAMOS RECURSOS

            gr.Dispose();

            //GUARDAMOS LA NUEVA IMAGEN ESPECIFICAMOS LA RUTA Y EL FORMATO

            nuevaImagen.Save(DirTemp, System.Drawing.Imaging.ImageFormat.Jpeg);

            //LIBERAMOS RECURSOS

            nuevaImagen.Dispose();

            imagen.Dispose();

            return DirTemp;

        }

    }
}