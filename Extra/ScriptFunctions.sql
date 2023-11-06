
### SESSIONS
	# nº Sessions


	# Avg lenght of Sesssion

### MONETISATION

	#ARPU
    
    #ARRPU

### NUMBER OF USERS
	
    #DAU
CREATE VIEW DAU AS 
select d.date , count(distinct s.User_Id) as  "DAU"
from  xaviercb12.dates d
left join xaviercb12.Sessions s
on  d.date= date(s.Start_Timestamp) 
group by d.date 

    #MAU

### RETENTION

	#D1
    
    #D7
    
    #DX1
    
    #DX2