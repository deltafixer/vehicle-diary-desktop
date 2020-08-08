namespace VehicleDiary.Models
{
    public class Enums
    {
        public class UserEnums
        {
            public enum Role
            {
                ADMIN, USER
            }

            public enum UserType
            {
                PERSON, SERVICE, TAXI, DRIVING_SCHOOL, POLICE
            }

            public enum ServiceType
            {
                AUTHORIZED, NOT_AUTHORIZED
            }

        }

        public class VehicleEnums
        {
            public enum Make

            {
                OPEL, AUDI, RENAULT
            }

            public enum Model
            {
                ASTRA_J, A1, MEGANE, LAGUNA
            }

            public enum Condition
            {
                NEW, USED, CERTIFIED
            }

            public enum BodyStyle
            {
                CONVERTIBLE, SEDAN, VAN_MINIVAN, COUPE, SUV_CROSSOVER, VAGON, HATCHBACK, TRUCK
            }

            public enum DriveType
            {
                AWD_4WD, FRONT, REAR
            }

            public enum EngineEmissionType
            {
                EURO_1, EURO_2, EURO_3, EURO_4, EURO_5, EURO_6
            }

            public enum FuelType
            {
                PETROL, DIESEL, PETROL_AND_TNG, CNG, ELECTRIC, HYBRID
            }

            public enum GearboxType
            {
                MANUAL_4, MANUAL_5, MANUAL_6, SEMI_AUTOMATIC, AUTOMATIC
            }
        }
    }
}