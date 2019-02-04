﻿using ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand;
using ApplicationLogic.Business.Commands.Sync.SyncFromDesktopCommand.Models;
using ApplicationLogic.SignalR;
using Framework.EF.DbContextImpl.Persistance.Paging.Models;
using Framework.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuickbooksIntegratorAPI.Auth;
using System.Collections.Generic;
using Authorization = Microsoft.AspNetCore.Authorization;

namespace QuickbooksIntegratorAPI.Controllers
{
    /// <summary>
    /// InventoryItem API interface
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/syncfromdesktop")]
    public class SyncFromDesktopController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemController"/> class.
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="pageQueryCommand">The page query command</param>
        /// <param name="getAllCommand">The get all command.</param>
        /// <param name="getByIdCommand">The get by identifier command.</param>
        /// <param name="insertCommand">The insert command.</param>
        /// <param name="updateCommand">The update command.</param>
        /// <param name="deleteCommand">The delete command.</param>
        public SyncFromDesktopController(IHubContext<GlobalHub, IGlobalHub> hubContext, ISyncFromDesktopCommand syncFromDesktopCommand): base(/*hubContext*/)
        {
            this.SignalRHubContext = hubContext;
            this.SyncFromDesktopCommand = syncFromDesktopCommand;
        }

        
        /// <summary>
        /// 
        /// </summary>
        public IHubContext<GlobalHub, IGlobalHub> SignalRHubContext { get; }

      
        /// <summary>
        /// Gets the insert command.
        /// </summary>
        /// <value>
        /// The insert command.
        /// </value>
        public ISyncFromDesktopCommand SyncFromDesktopCommand { get; }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost("fromDesktop"), ProducesResponseType(200, Type = typeof(SyncFromDesktopCommandOutputDTO))]
        //[Authorization.Authorize(Policy = PermissionsEnum.InventoryItem_Manage)]
        public IActionResult Post([FromBody]SyncFromDesktopCommandInputDTO model)
        {
            var appResult = this.SyncFromDesktopCommand.Execute(model);
            if(appResult.IsSucceed)
            {
                var signalArgs = new SignalREventArgs(SignalREvents.DATA_CHANGED.Identifier, nameof(SignalREvents.DATA_CHANGED.ActionEnum.ADDED_ITEM), nameof(DomainModel.InventoryItem), appResult);
                this.SignalRHubContext.Clients.All.DataChanged(signalArgs);

            }
            return appResult.IsSucceed ? (IActionResult)this.Ok(appResult) : (IActionResult)this.BadRequest(appResult);
        }

    }
}
