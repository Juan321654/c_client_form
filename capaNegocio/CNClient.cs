using capaEntidad;
using capaDatos;
using System.Data;

namespace capaNegocio
{
    public class CNClient
    {
        CDClient cDClient = new CDClient();
        public bool ValidateData(CEClients client)
        {
            bool result = true;
            if (client.Name == "")
            {
                result = false;
                MessageBox.Show("Name required");
            }

            if (client.LastName == "")
            {
                result = false;
                MessageBox.Show("Last name required");
            }

            if (client.PhotoPath == null)
            {
                result = false;
                MessageBox.Show("Image required.");
            }

            return result;
        }

        public void CheckMySql()
        {
            cDClient.checkConnection();
        }

        public void CreateClient(CEClients cE)
        {
            cDClient.Create(cE);
        }

        public void UpdateClient(CEClients cE)
        {
            cDClient.Update(cE);
        }

        public void DeleteClient(CEClients cE)
        {
            cDClient.Delete(cE);
        }

        public DataSet GetData()
        {
            return cDClient.GetList();
        }
    }
}
