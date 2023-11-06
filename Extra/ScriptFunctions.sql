
### SESSIONS
	# nº Sessions


	# Avg lenght of Sesssion

### MONETISATION

	#ARPU
    
    #ARRPU

### NUMBER OF USERS
	
    #DAU
    /*
CREATE VIEW DAU AS 
select d.date , count(distinct s.User_Id) as  "DAU"
from  xaviercb12.dates d
left join xaviercb12.Sessions s
on  d.date= date(s.Start_Timestamp) 
group by d.date ;
*/
    #MAU
    /*
CREATE VIEW MAU AS 
select d.date , count(distinct s.User_Id) as MAU
from xaviercb12.dates d
left join xaviercb12.Sessions s
on datediff(s.Start_Timestamp, d.date)<=1 and  datediff(s.Start_Timestamp, d.date)>-30
group by d.date;
*/
### RETENTION
	
    #Stickiness
    /*
select d.date, DAU/MAU *100 AS "STICKINESS"
from xaviercb12.DAU d
left join  xaviercb12.MAU m
on d.date = m.date
GROUP BY d.date
*/
	#D1
    /*
select count(distinct s.player_id)/ count(distinct u.user_id)
from test.users u 
left join test.sessions s
on u.user_id = s.player_id  and DATE(s.start - interval 7 day) = u.dateCreated
*/
    #D7
    
    #DX1
    
    #DX2