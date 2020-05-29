using System;
using Microsoft.AspNetCore.SignalR;
using ModelsBaseData;
using REST_API.Hubs;
using REST_API.SQLDependency.Interface;
using TableDependency.SqlClient;

using TableDependency.SqlClient.Base.Enums;

namespace REST_API.SQLDependency
{
    public class HmiMgmtExchangeDatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;

        private readonly IHubContext<HmiMgmtExchangeHub> _hubContext;
        private SqlTableDependency<HmiMgmtExchange> _tableDependency;


        // CONSTRUCTOR
        public HmiMgmtExchangeDatabaseSubscription(IHubContext<HmiMgmtExchangeHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            // Tabel Dependency instellen voor Products tabel
            _tableDependency = new SqlTableDependency<HmiMgmtExchange>(connectionString);
            _tableDependency.OnChanged += _tableDependency_OnChanged;
            _tableDependency.OnError += _tableDependency_OnError;
            _tableDependency.Start();
        }


        private void _tableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        // Error bij Table dependency
        {
            //  throw new NotImplementedException();
        }

        private void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<HmiMgmtExchange> e)
        // Er is een wijziging in de tabel
        {

            string groepNaam = e.Entity.Machine; // Groep identifer van Clients. ==> Enkel clients met dia aangesloten zijn aan een grope met dezelfde naam ontvangen een melding.

            switch (e.ChangeType)
            {
                case ChangeType.None:
                    // Doe niets
                    break;
                case ChangeType.Delete:
                    // Aangepast Object versturen naar alle clients in groep met naam: "groepNaam"
                    _hubContext.Clients.Group(groepNaam).SendAsync("DeletedItem", e.Entity);
                    break;
                case ChangeType.Insert:
                    // Aangepast Object versturen naar alle clients in groep met naam: "groepNaam"
                    _hubContext.Clients.Group(groepNaam).SendAsync("InsertedItem", e.Entity);
                    break;
                case ChangeType.Update:
                    // Aangepast Object versturen naar alle clients in groep met naam: "groepNaam"
                    _hubContext.Clients.Group(groepNaam).SendAsync("UpdatedItem", e.Entity);
                    break;
                default:
                    break;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
