using FullDevToolKit.Core;
using FullDevToolKit.Common;
using FullDevToolKit.ApplicationHelpers;
using System.Text;

namespace MyApp.Core.Manager
{
    public class MyApptMailCenter : MailManager
    {
       
        public MyApptMailCenter(ISettings settings)
        {
            Config = settings.MailSettings;
            Config.ContentEncoding = Encoding.UTF8;
        }

        public void Initialize()
        {

        }



        public ExecutionStatus SendTemporaryPassword(string email, string name, string code)
        {
            ExecutionStatus ret = new ExecutionStatus(true);
            StringBuilder strq = new StringBuilder();
            strq.Append("<b>SOLICITAÇÃO DE SENHA TEMPORÁRIA</b>" + "<BR/> <BR/>");
            strq.Append("Esta é a sua senha temporária: " + "<BR/>");
            strq.Append("<b>" + code + "</b><BR/>");
            strq.Append("Utilize essa senha pra acessar o site. ");
            strq.Append("Após acessar, recomendamos que você redefina sua senha.");
           
            ret.Success = Send(name, email, Config.NameSender + " - Recuperação de Senha", strq.ToString());

            return ret;
        }

        public ExecutionStatus SendActiveAccountCode(string email, string name, string code)
        {
            ExecutionStatus ret = new ExecutionStatus(true);
            StringBuilder strq = new StringBuilder();

            strq.Append("<b>ATIVAÇÃO DE CONTA</b>" + "<BR/> <BR/>");
            strq.Append("Este é o código para a ativação de conta:" + "<BR/>");
            strq.Append("<b>" + code + "</b><BR/>");
            strq.Append("Acesse o site e utilize esse código pra ativação da conta.");
          
            ret.Success = Send(name, email, Config.NameSender + " - Ativação de Conta", strq.ToString());

            return ret;
        }

        public ExecutionStatus SendChangePassowordCode(string email, string name, string code)
        {
            ExecutionStatus ret = new ExecutionStatus(true);
            StringBuilder strq = new StringBuilder();
            strq.Append("<b>REDEFINIÇÃO DE SENHA</b>" + "<BR/> <BR/>");
            strq.Append("Este é o código de autorização para a troca da senha:" + "<BR/>");
            strq.Append("<b>" + code + "</b><BR/>");
            strq.Append("Acesse o site e utilize esse código pra trocar a sua senha.");
            
            ret.Success = Send(name, email, Config.NameSender + " - Redefinição de Senha", strq.ToString());

            return ret;
        }

        public ExecutionStatus SendEmailConfirmationCode(string email, string name, string code)
        {
            ExecutionStatus ret = new ExecutionStatus(true);
            StringBuilder strq = new StringBuilder();
            strq.Append("<b>CONFIRMAÇÃO DE E-MAIL</b>" + "<BR/> <BR/>");
            strq.Append("Bem-vindo ao Portal " + Config.NameSender + "<BR/>");
            strq.Append("Utilize o código abaixo para confirmar seu cadastro no site o Portal." + "<BR/>");
            strq.Append("<b>" + code + "</b><BR/>");
            strq.Append("Prossiga com a confirmação do cadastro no site.");

            ret.Success = Send(name, email, Config.NameSender + " - Confirmação de Cadastro", strq.ToString());

            return ret;
        }

    }
}
