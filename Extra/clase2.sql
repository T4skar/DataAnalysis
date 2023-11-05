#select concat (firstName," ", lastName) as "New_Name" from kpi.users #CREAS UNA NUEVA TABLA
#select concat (firstName," ", lastName) from kpi.users where country="china"

#select concat (firstName," ", lastName) from kpi.users where firstName LIKE "P%" #%PARA CUALQUIER CARACTER Q VENGA ESPUES
#select firstName ,lastName from kpi.users where firstName LIKE "B%" AND lastName LIKE "B%" #%PARA CUALQUIER CARACTER Q VENGA ESPUES
#select COUNT( country) from kpi.users WHERE country="china"#PARA CONTAR, el distinct para decir de caunto de eso hay
#select country, count(country) from kpi.users group by 1 # 1 para decirle q vuelva a coger a mismsa variable del principio
#select sex, count(country) from kpi.users group by 1 # 1 para decirle q vuelva a coger a mismsa variable del principio
#select country, count(country) from kpi.users group by 1 order by 2 desc # poner priemro 1 y dedspues 2 
#count no cuenta null
#timestampdiff (unit, colname1, colname2) from tblname

#SELECT player_id, timestampdiff(minute,start, end) as "LenghtSession"from kpi.sessions;

#select t1.user_id,  avg(timestampdiff(minute,t2.start, t2.end))   
#from kpi.users t1, kpi.sessions t2 where t1.user_id = t2.player_id
#group by 1

#select * from kpi.sessions s, kpi.users u where s.player_id = u.user_id #no es eficiente
#select * from kpi.sessions s, kpi.users u where s.player_id = u.user_id
#Join


#select t1.firstName,count(t2.session_id) from kpi.users t1 join kpi.sessions t2 on t1.user_id = t2.player_id group by 1;

#select u.firstname	, count(s.session_id) 
#from kpi.users u
#left join kpi.sessions s
#on u.user_id = s.player_id	and u.country ="China"
#group by u.user_id
#order by 2 desc
#select country, count(country) from kpi.users group by 1;

#ej1
#select u.country , count(s.session_id) 
#from kpi.users u
#left join kpi.sessions s
#on u.user_id = s.player_id	
#group by u.country
#order by 2 desc;

#ej2
#select u.country , avg(timestampdiff(minute,s.start, s.end))
#from kpi.users u
#right join kpi.sessions s
#on u.user_id = s.player_id	
#group by u.country
#order by 2 desc;

#ej2
#select u.country , avg(timestampdiff(minute,s.start, s.end))
#from kpi.users u
#right join kpi.sessions s
#on u.user_id = s.player_id	
#group by u.country
#order by 2 desc;

#ej2 RIGHT --> ARPPU
#select sum(t.totalPrice) , count(distinct t.player_id), sum(t.totalPrice)/count(distinct t.player_id) as "ARRPU"
#from kpi.users u
#right join kpi.transactions t
#on u.user_id = t.player_id	

#ej2  LEFT-->ARPU;
#select sum(t.totalPrice) , count(distinct t.player_id), sum(t.totalPrice)/count(distinct t.player_id) as "ARPU"
#from kpi.users u
#left join kpi.transactions t
#on u.user_id = t.player_id	;

#create view sessionusers as
#select * from kpi.sessions join kpi.users on user_id = player_id

#select country, count(distinct session_id)
#from kpi.sess

#D1
#select count(u.user_id)/count(DATE("u.start" - interval 1 day) = u.dateCreated)
select count(distinct s.player_id)/ count(distinct u.user_id)
from kpi.users u 
left join kpi.sessions s
on u.user_id = s.player_id  and DATE(s.start - interval 7 day) = u.dateCreated



