using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace API.Definitions.Conventions
{
    public class CustomControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // Convertir el nombre del controlador a minúsculas
            string controllerName = controller.ControllerType.Name.ToLower();

            // Remover la parte "Controller" del nombre del controlador
            if (controllerName.EndsWith("controller"))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - "controller".Length);
            }

            // Remover la parte "AppService" del nombre del controlador
            if (controllerName.EndsWith("appservice"))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - "appservice".Length);
            }

            // Agregar la ruta al atributo de ruta del controlador
            controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
            {
                Template = $"api/{controllerName}"
            };
        }
    }
}