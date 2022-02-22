using Microsoft.AspNetCore.Mvc;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Filee = System.IO.File;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RFD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public ComponentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        //[HttpPost("add")]
        //public async Task<ApiResponse<Component>> AddAsync(Component model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return ApiResponse<Component>.Fail("Veri formatı düzgün biçimde değildi.");

        //    }

        //    (bool result, Component component) = await _unitOfWork.ComponentService.IsExist(model.ComputerName);

        //    if (result)
        //    {
        //        // await _computerService.UpdateAsync(model);
        //        return ApiResponse<Component>.Fail("Bigisayar zaten sisteme kayıtlı");
        //    }


        //    var entity = await _unitOfWork.ComponentService.AddAsync(model);
        //    var complate = await _unitOfWork.Complete();
        //    if (complate > default(int))
        //    {
        //        string appName = "RFDDesktop.exe";
        //        string appPath = @"D:\tatbikat";
        //        string copyPath = $@"\\{entity.ComputerName}\c$\ProgramData\tatbikat";
        //        ProcessStartInfo startInfo1 = new ProcessStartInfo("cmd.exe");
        //        startInfo1.Arguments = $@"/c C:\Windows\System32\robocopy.exe  {appPath} {copyPath}";
        //        startInfo1.Verb = "runas";
        //        startInfo1.UseShellExecute = false;
        //        startInfo1.CreateNoWindow = true;
        //        var proc = Process.Start(startInfo1);
        //        //  proc.WaitForExit(3000);
        //        if (proc.WaitForExit(3000))
        //        {
        //            if (Filee.Exists(copyPath + "\\" + appName))
        //            {
        //                ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "/PsExec.exe");
        //                startInfo.Arguments = $@"\\{entity.ComputerName} -s -i  C:\ProgramData\tatbikat\{appName}";
        //                startInfo.UseShellExecute = false;
        //                startInfo.Verb = "runas";
        //                var proc1 = Process.Start(startInfo);
        //                proc1.WaitForExit(3000);
        //            }
        //        }
        //    }

        //    return ApiResponse<Component>.Success(entity);

        //}



        [HttpGet("get-all")]
        public async Task<ApiResponse<IEnumerable<Component>>> GetAllAsync(string filter)
        {
            var component = await _unitOfWork.ComponentService.GetAllAsync();
            if (component.Count() == default(int))
            {
                return ApiResponse<IEnumerable<Component>>.Fail("Kayıt bulunamadı");
            }
            return ApiResponse<IEnumerable<Component>>.Success(component);
        }






      

        // PUT api/<DriffController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DriffController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
