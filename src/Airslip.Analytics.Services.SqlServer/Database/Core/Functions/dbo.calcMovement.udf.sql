create or alter function calcMovement (@curr as bigint, @prev as bigint)
    returns numeric(10, 2)
    as begin
   return (@curr-@prev)*100.0/@prev
end