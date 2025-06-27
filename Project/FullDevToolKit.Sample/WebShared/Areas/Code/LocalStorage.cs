using Newtonsoft.Json;
using Microsoft.JSInterop;
using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;

namespace MyApp.ServerCode
{
    public class LocalStorage
    {

        private IJSRuntime _jsruntime;
        ServerFunctions utils = new ServerFunctions();

        public LocalStorage(IJSRuntime jsruntime)
        {
            _jsruntime = jsruntime;

        }
        //


        public async Task SetSession(UserAuthenticated user)
        {

            string val = JsonConvert.SerializeObject(user);

            await utils.SaveLocalData(_jsruntime, "SESSION", val);

        }

        public async Task<UserAuthenticated> GetSession()
        {
            UserAuthenticated ret = null;

            string aux = await utils.ReadLocalData(_jsruntime, "SESSION");

            if (aux != null)
            {
                ret = JsonConvert.DeserializeObject<UserAuthenticated>(aux);
            }

            return ret;
        }

        public async Task ClearSession()
        {
            await utils.SaveLocalData(_jsruntime, "SESSION", null);
        }



        //

        public async Task SetUserPermissions_(List<UserPermissions> permissions, string token)
        {

            // adicionando uma camada de segurança; criar um registro de permissão com o token

            permissions.Add(new UserPermissions() { PermissionID = 0, ObjectCode = token });

            string val = JsonConvert.SerializeObject(permissions);            

            await utils.SaveLocalData(_jsruntime, "USERPERMISSIONS", val);

        }

        public async Task<List<UserPermissions>> GetUserPermissions_(string token)
        {
            List<UserPermissions> ret = null;

            string aux = await utils.ReadLocalData(_jsruntime, "USERPERMISSIONS");

            if (aux != null)
            {
                ret = JsonConvert.DeserializeObject<List<UserPermissions>>(aux);

                //verificando o token...
                var obj = ret.Where(x=>x.PermissionID == 0).FirstOrDefault();
                if (obj.ObjectCode==token)
                {
                    ret.Remove(obj); 
                }
                else
                {
                    ret = new List<UserPermissions>(); // caso o token não coincida, retornar a lista de permissões vazia
                }
            }

            return ret;
        }

        public async Task ClearUserPermissions_()
        {
            await utils.ClearLocalData(_jsruntime, "USERPERMISSIONS");
        }

        // 

        public async Task SetMenuConfigs(List<MenuItemConfigs> items)
        {            

            string val = JsonConvert.SerializeObject(items);

            await utils.SaveLocalData(_jsruntime, "MENUCONFIGS", val);

        }

        public async Task<Dictionary<string,MenuItemConfigs>> GetMenuConfigs()
        {
            Dictionary<string, MenuItemConfigs> ret = new Dictionary<string, MenuItemConfigs>();

            List<MenuItemConfigs> list = null; 

            string aux = await  utils.ReadLocalData(_jsruntime, "MENUCONFIGS");

       
            if (aux != null)
            {
                list = JsonConvert.DeserializeObject<List<MenuItemConfigs>>(aux);

                foreach (MenuItemConfigs i in list)
                {
                    ret.Add(i.Key, i);

                }
            }

            return ret;
        }

        public async Task ClearMenuConfigs()
        {
            await utils.ClearLocalData(_jsruntime, "MENUCONFIGS");
        }

    }

}
