create or alter function round5min
(
    @date datetime
) returns datetime
as
begin -- adding 150 seconds to round off instead of truncating
return dateadd(minute, datediff(minute, '1900-01-01', dateadd(second, 150, @date))/5*5, 0)
end