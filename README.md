# CBS
CBS (Custom Booking System) is implemented as a self-hosted web service.
It contains two methods: one for maing a reservation and one for finalizing it.
The main flow is: The customer makes a reservation and gets a reservation id which is then used to update the reservation with return date and passed kilometers.

# To run:
1. Build CBSSqlRepositories project.
2. Modify the "CbsConnection" conection string in App.config in CBS project. The database is generated and seeded automatically in the location provided in "AttachDbFileName" property. "Data Source" property should poitn to sql server instance. 
3. A REST tool can be used to communicate with the service. E.g. Postman for Chrome or Fiddler. 
4. Example calls:
   POST to localhost:8080/vehicle/book with body: (Returns reservation ID that is used in the PATCH)
   
                            {
                              "CustomerNumber": "1231241",
                              "VehicleType" : 2,
                              "BookingDate": "2017-02-21 23:09:58",
                              "BookingKilometers": 200
                            }
                            
    PATCH to localhost:8080/vehicle/finalize with body 
    
                            {
                                "ReservationId": 1,
                                "ReturnDate": "2017-02-21 23:10:58",
                                "ReturnKilometers": 4000
                              }
                              
                             
# Configuration:
The repositories are injected so they can be replaced by any module that implements IExternalReservationRepository and IExternalSettingRepository.
Steps to add a new data layer:
1. Create an assembly with classes that implement the IExternalReservationRepository and IExternalSettingRepository
2. In CBS App.config in the <unity> section add the new assembly name.
3. Currently the new DLLs should be in the bin/debug of their own project in the CBS solution directory. There is a TODO in UnityContainerFactory for that.  
4. There is already one Entity Framework implementation of the data layer in CBSSqlRepositories.

# Notes:
The service is self-hosted with MS Owin implementation so no IIS is needed. The port can be changed in the Program.cs file.
The PricePerDay and PricePerKilometer settings are in the database and have some initial value. 
The database will be recreated and reseeded if the models change so dont rely on it being too persistant. 
