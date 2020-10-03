
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DojoNetCore.Modelo;
using Google.Cloud.Firestore;
namespace DojoNetCore
{
    public class FireBaseAccount
{
    private readonly static FireBaseAccount _instancia=new FireBaseAccount();
    FirestoreDb _db;
    public FireBaseAccount()
    {
        String path = AppDomain.CurrentDomain.BaseDirectory + @"Firebase-SDK.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
        _db = FirestoreDb.Create("dojo-net-core");
        Console.WriteLine("Se conecto correctamente");
    }
    public static FireBaseAccount instancia
    {
        get
        {
            return _instancia;
        }
    }

    public async Task<String> AddUser(usuario user)
    {
        DocumentReference coll=_db.Collection("Usuarios").Document();
        Dictionary <String,object> data=new Dictionary<string,object>(){
            {"Cedula", user.Cedula},
            {"Nombre", user.Nombre},
            {"Correo", user.Correo},
            {"Carrera", user.Carrera}
        };
        await coll.SetAsync(data);
        return "Usuario guardado con id: " +coll.Id;
    }
    public async Task<List<usuario>> GetUser()
    {
        CollectionReference userRef=_db.Collection("Usuarios");
        QuerySnapshot queryUser=await userRef.GetSnapshotAsync();
        List<usuario> userList = new List<usuario>();
        foreach(DocumentSnapshot documentSnapshot  in queryUser.Documents)
        {
            Dictionary <String,Object> usuario = documentSnapshot.ToDictionary();
            usuario user = new usuario();
            foreach(var item in usuario)
            {
                if (item.Key=="Nombre")
                {
                    user.Nombre=(string)item.Value;
                }else if(item.Key=="Cedula")
                {
                    user.Cedula=(string)item.Value;
                }else if(item.Key=="Correo")
                {
                    user.Correo=(string)item.Value;
                }else if(item.Key=="Carrera")
                {
                    user.Carrera=(string)item.Value;
                }
            }
            userList.Add(user);
        }
        return userList;
    } 

    public async Task<String> DeleteUser (String id)
    {
        DocumentReference userDelete = _db.Collection("Usuarios").Document(id);
        await userDelete.DeleteAsync();
        return "Usuario Deleteado con id: "+id; 
    }
}
}