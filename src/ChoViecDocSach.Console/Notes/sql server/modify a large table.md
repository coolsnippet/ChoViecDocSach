<!-- TOC insertAnchor:true orderedList:true -->


<!-- /TOC -->

1. modify the SSMS in Tools -> Options, Designers, remove checkbox "Override connection ... " or give the big number to it like 65535 (not effective way, use the below)
2. please use the sql statement instead like:
alter table table)name
      add column_name column-definition;
go      

alter table employee
      add last_name varchar(50);
go

alter table employee alter column last_name varchar(51);
go

