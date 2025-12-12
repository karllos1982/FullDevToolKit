using Microsoft.FluentUI.AspNetCore.Components;
using MyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUIComponents.Common
{
    

    public  class PageFormMethods
    {

        private BaseViewModel view;

        private IDialogService msgbox;

        private TaskLoading loading; 

        public PageFormMethods()
        {

        }

        public PageFormMethods(BaseViewModel viewmodel, 
            IDialogService msg, TaskLoading loadingcontrol)
        {
            view = viewmodel;
            msgbox = msg;
            loading = loadingcontrol;   
        }


        public async Task OnSearch()
        {            
            await view.Search();

            if (!view.ServiceStatus.Success)
            {
                await msgbox.ShowWarningAsync(view.ServiceStatus.Exceptions.Messages[0].Description,
                    view.texts.Get("ErrorOnExecuteSearch"));
            }            

        }

        public void OnNew()
        {
            view.InitNew();            
        }

        public async Task OnGet(object id)
        {
            await loading.Begin();

            await view.Get(id);

            await loading.End();

            if (!view.ServiceStatus.Success)
            {
                await msgbox.ShowWarningAsync(view.ServiceStatus.Exceptions.Messages[0].Description,
                    view.texts.Get("ErrorOnReturnData"));

            }
            else
            {
                view.InitEdit();

            }
            
        }

        public async Task OnSet()
        {

            await view.Set();

            if (!view.ServiceStatus.Success)
            {
                await msgbox.ShowWarningAsync(view.ServiceStatus.Exceptions.Messages[0].Description,
                    view.texts.Get("NoticeLabel"));

            }
            else
            {
                await msgbox.ShowSuccessAsync(view.texts.Get("SuccessSaveMessage"),
                    view.texts.Get("SuccessLabel"));

                this.Back();

            }


        }

        public async Task OnDetClick(object id)
        {
            await OnGet(id);

        }

        public void Back()
        {
            view.BackToSearch();            

        }

        public async Task OnRemove()
        {

            var dialog = await msgbox.ShowConfirmationAsync(view.texts.Get("RemoveConfirmation-Label"));
            var result = await dialog.Result;
            bool canceled = result.Cancelled;

            if (!canceled)
            {

                await view.Remove();

                if (!view.ServiceStatus.Success)
                {
                    await msgbox.ShowWarningAsync(view.ServiceStatus.Exceptions.Messages[0].Description,
                        view.texts.Get("NoticeLabel"));
                }
                else
                {
                    await msgbox.ShowSuccessAsync(view.texts.Get("RemoveConfirmation-Message"),
                        view.texts.Get("SuccessLabel"));

                    await view.Search();
                    this.Back();
                }
            }

        }

    }
}
