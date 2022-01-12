create or alter function getId ()
    returns varchar(33)
    as begin
   return (select replace(new_id, '-', '') from getNewID)
end