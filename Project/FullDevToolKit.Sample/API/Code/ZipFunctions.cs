using GW.Common;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Ionic.Zip;
using System.IO;

namespace API.Code
{
    public class ZipFunctions
    {
        
        public static async Task<OperationStatus> GerarArquivoZip(string zipfilename,
             List<string> files)
        {
            OperationStatus ret = new OperationStatus(true);
            
            await Task.Run(() =>
            {

                using (ZipFile zip = new ZipFile())
                {
                    // percorre todos os arquivos da lista
                    foreach (string item in files)
                    {
                        // se o item é um arquivo
                        if (File.Exists(item))
                        {
                            try
                            {
                                // Adiciona o arquivo na pasta raiz dentro do arquivo zip
                                zip.AddFile(item, "");
                            }
                            catch (Exception ex)
                            {
                                ret.Status = false;
                                ret.Error = ex;
                            }
                        }

                    }
                    // Salva o arquivo zip para o destino
                    try
                    {
                        zip.Save(zipfilename);
                    }
                    catch(Exception ex)
                    {
                        ret.Status = false;
                        ret.Error = ex;
                    }
                }

            });

            return ret;
        }

    

    }
}
