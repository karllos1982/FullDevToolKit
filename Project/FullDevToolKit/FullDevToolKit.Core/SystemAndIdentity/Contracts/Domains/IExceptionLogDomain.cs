﻿using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Domains
{
    public interface IExceptionLogDomain :
        IDomain<ExceptionLogParam, ExceptionLogEntry, ExceptionLogList, ExceptionLogResult>
    {
        

    }

}