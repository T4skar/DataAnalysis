
### SESSIONS
	# nº Sessions
    /*
select count(s.session_id) AS "Nº of Sessions"
from xaviercb12.Users u
left join xaviercb12.Sessions s
on u.User_Id = s.User_Id	
*/
	# Avg lenght of Sesssion
select  u.User_Country, avg(timestampdiff(minute,s.Start_Timestamp, s.End_Timestamp)) AS "AVG time of Session per Country"
from xaviercb12.Users u
right join xaviercb12.Sessions s
on u.User_Id = s.User_Id	
group by u.User_Country


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
select count(distinct s.User_Id)/ count(distinct u.User_Id) AS D1
from xaviercb12.Users u 
left join xaviercb12.Sessions s
on u.User_Id = s.User_Id  and DATE(s.Start_Timestamp - interval 1 day) =  date(u.Sign_Up_Time)  ;
*/


    #D7
     /* 
select count(distinct s.player_id)/ count(distinct u.user_id) AS D7
from test.users u 
left join test.sessions s
on u.user_id = s.player_id  and DATE(s.start - interval 7 day) = u.dateCreated;
*/
    #D13
      /*
select count(distinct s.player_id)/ count(distinct u.user_id) AS D13
from test.users u 
left join test.sessions s
on u.user_id = s.player_id  and DATE(s.start - interval 13 day) = u.dateCreated;
*/
    #D56
      /*
select count(distinct s.player_id)/ count(distinct u.user_id) AS D17
from test.users u 
left join test.sessions s
on u.user_id = s.player_id  and DATE(s.start - interval 56 day) = u.dateCreated;
*/