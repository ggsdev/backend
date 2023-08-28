namespace PRIO.src.Shared.Errors
{
    public class ErrorMessages
    {
        private static readonly string _well = "Well";
        private static readonly string _cluster = "Cluster";
        private static readonly string _installation = "Installation";
        private static readonly string _field = "Field";
        private static readonly string _reservoir = "Reservoir";
        private static readonly string _completion = "Completion";
        private static readonly string _zone = "Zone";
        private static readonly string _user = "User";
        private static readonly string _equipment = "MeasuringEquipment";
        private static readonly string _measuringPoint = "MeasuringPoint";
        private static readonly string _production = "Production";
        private static readonly string _nfsm = "NFSM";
        private static readonly string _commentInProduction = "CommentInProduction";

        //private readonly IStringLocalizer<ErrorMessages> _localizer;

        //public ErrorMessages(IStringLocalizer<ErrorMessages> localizer)
        //{
        //    _localizer = localizer;
        //}
        public static string NotFound<TModel>()
        {
            var message = " não encontrado(a).";

            if (typeof(TModel).Name == _cluster)
                return "Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Equipamento de medição" + message;

            if (typeof(TModel).Name == _measuringPoint)
                return "Ponto de medição" + message;

            if (typeof(TModel).Name == _user)
                return "Usuário" + message;

            if (typeof(TModel).Name == _production)
                return "Produção" + message;

            if (typeof(TModel).Name == _commentInProduction)
                return "Comentário" + message;

            if (typeof(TModel).Name == _nfsm)
                return "Notificação de falha" + message;

            return message;

            //return _localizer[$"{typeof(TModel).Name}NotFound"];
        }

        public static string InactiveAlready<TModel>()
        {
            var message = " já está inativo(a).";

            if (typeof(TModel).Name == _cluster)
                return "Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Equipamento de medição" + message;

            if (typeof(TModel).Name == _production)
                return "Produção" + message;

            return message;
        }

        public static string ActiveAlready<TModel>()
        {
            var message = " já está ativo(a).";

            if (typeof(TModel).Name == _cluster)
                return "Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Equipamento de medição" + message;



            return message;
        }

        public static string UpdateToExistingValues<TModel>()
        {
            var message = " já tem esses valores, tente atualizar para outros.";

            if (typeof(TModel).Name == _cluster)
                return "Esse Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Essa Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Esse Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Essa Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Esse Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Esse Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Essa Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Esse Equipamento de medição" + message;

            if (typeof(TModel).Name == _commentInProduction)
                return "Esse Comentário" + message;

            return message;
        }


        public static string CodAlreadyExists<TModel>()
        {
            var message = " já possui esse código, tente outro.";

            if (typeof(TModel).Name == _cluster)
                return "Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Equipamento de medição" + message;
            return message;
        }
        public static string CodCantBeUpdated<TModel>()
        {
            var message = " não pode ter o código alterado.";

            if (typeof(TModel).Name == _cluster)
                return "Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Equipamento de medição" + message;
            return message;
        }

        public static string DifferentFieldsCompletion()
        {
            return "Poço e reservatório devem pertencer ao mesmo campo.";
        }

        public static string WellAndReservoirAlreadyCompletion()
        {
            return "Já existe uma completação com esse poço e reservatório associados";
        }

        public static string Inactive<TModel>()
        {
            var message = " está inativo(a).";

            if (typeof(TModel).Name == _cluster)
                return "Cluster" + message;

            if (typeof(TModel).Name == _installation)
                return "Instalação" + message;

            if (typeof(TModel).Name == _field)
                return "Campo" + message;

            if (typeof(TModel).Name == _zone)
                return "Zona" + message;

            if (typeof(TModel).Name == _reservoir)
                return "Reservatório" + message;

            if (typeof(TModel).Name == _well)
                return "Poço" + message;

            if (typeof(TModel).Name == _completion)
                return "Completação" + message;

            if (typeof(TModel).Name == _equipment)
                return "Equipamento de medição" + message;

            if (typeof(TModel).Name == _production)
                return "Produção" + message;

            return message;
        }
    }
}