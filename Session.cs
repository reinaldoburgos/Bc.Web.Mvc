using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Dece.Common.Security;

namespace Bc.Web.Mvc
{
    public class Session
    {
        public bool IsStarted { get; set; }
        public static bool IsHttpContext { get; set; }
        public static bool ValidateActivityPermissions { get; set; }
        public static bool IsClosing { get; set; }
        public static string UpdaterPath { get; set; }

        public bool NotificationOnLine { get; set; }
        public bool UsaModuloIB { get; set; }

        public void Clear()
        {
            Current.IsStarted = false;
            Current.currentOrganization.Id = 0;
            Current.currentUser.LogonName = null;
            Current.currentUser.FullName = null;
            Current.currentConnection.Token = null;
            Current.CurrentOption = 0;
            Current.CurrentActivity = string.Empty;
            Current.UsaModuloIB = false;

            Current.currentProfile.IdPersona = 0;
            Current.currentProfile.IdEstudiante = 0;

            //Current.AnioLectivo = 0;

            if (Current.ListAccion != null)
                Current.ListAccion.Clear();

            if (Current.ListActividad != null)
                Current.ListActividad.Clear();

            if (Current.ListOpcion != null)
                Current.ListOpcion.Clear();

            if (Current.currentProfile.EntityProfile != null)
                Current.currentProfile.EntityProfile.Clear();

            //SetSessionValue("Session", null);
        }


        //static Session current = new Session();


        public static Session Current
        {
            get
            {
                object session = GetSessionValue("Session");

                if (session == null)
                {
                    session = new Session();
                    SetSessionValue("Session", session);
                }

                return (Session)session;
            }


            //get
            //{
            //    if (IsHttpContext)
            //    {
            //        object session = GetSessionValue("Session");

            //        if (session == null)
            //        {
            //            session = new Session();
            //            SetSessionValue("Session", session);
            //        }

            //        return (Session)session;
            //    }
            //    else
            //    {
            //        if (current == null)
            //        {
            //            current = new Session();
            //        }

            //        return current;
            //    }
            //}

            internal set
            {
                SetSessionValue("Session", value);

                //if (IsHttpContext)
                //{
                //    SetSessionValue("Session", value);
                //}
                //else
                //{
                //    current = value;
                //}
            }
        }

        public static object GetSessionValue(string name)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Session[name];
            else
                return null;
        }

        public static void SetSessionValue(string name, object value)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session.Add(name, value);
        }

        User currentUser = new User();
        Organization currentOrganization = new Organization();
        TiendaActual currentTienda = new TiendaActual();
        Connection currentConnection = new Connection();
        Profile currentProfile = new Profile();

        AnioLectivoActual currentAnioLectivo = new AnioLectivoActual();

        public User CurrentUser { get { return currentUser; } }
        public Organization CurrentOrganization { get { return currentOrganization; } }
        public TiendaActual CurrentTienda { get { return currentTienda; } }
        public Connection CurrentConnection { get { return currentConnection; } }
        public Dece.Common.Security.Profile CurrentProfile { get { return currentProfile; } }

        public AnioLectivoActual CurrentAnioLectivo { get { return currentAnioLectivo; } }

        MessageCollection messages = new MessageCollection();
        public MessageCollection Messages { get { return messages; } }

        public string TextoAdicional { get; set; }

        public int CurrentOption { get; set; }
        public string CurrentActivity { get; set; }

        public List<OpcionQuery> ListOpcion { get; set; }
        public List<OpcionActividadQuery> ListActividad { get; set; }
        public List<OpcionActividadAccionQuery> ListAccion { get; set; }

        public string Menu { get; set; }
        //public int AnioLectivo { get; set; }
        //public string IdEstadoAnioLectivo { get; set; }

        public bool EsRepresentante
        {
            get
            {
                bool esRepresentante = false;

                var entity = currentProfile.EntityProfile;
                if (entity != null)
                {
                    esRepresentante = entity.Any(p => p.IdTipoentidad == "REP");
                }

                return esRepresentante;
            }
        }


        public bool EsInterno
        {
            get
            {
                bool esInterno = false;

                var entity = currentProfile.EntityProfile;
                if (entity != null)
                {
                    esInterno = entity.Any(p => p.AditionalValues == "1");
                }

                return esInterno;
            }
        }


        public bool EsExterno
        {
            get
            {
                bool esExterno = false;

                var entity = currentProfile.EntityProfile;
                if (entity != null)
                {
                    esExterno = entity.Any(p => p.AditionalValues == "2" || p.AditionalValues == "3") || currentProfile.EsEstudiante;
                }

                return esExterno;
            }
        }

    }

    public class User
    {
        public string LogonName { get; set; }
        public string FullName { get; set; }
    }

    public class Organization
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Logo { get; set; }
    }

    //public class Profile
    //{
    //    public int IdPersona { get; set; }
    //    public int IdEstudiante { get; set; }
    //}

    //public class EntityProfile
    //{
    //    public string IdTipoentidad { get; set; }
    //}

    public class Connection
    {
        public string TokenType { get; set; }
        public string Token { get; set; }
        public string UrlAPi { get; set; }
    }

    public class TiendaActual
    {
        public int IdTienda { get; set; }
        public string Descripcion { get; set; }
    }


    public class AnioLectivoActual
    {
        public int AnioLectivo { get; set; }
        public string Descripcion { get; set; }
        public string IdEstado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
