SELECT DISTINCT(i.value * j.value * k.value)
FROM input i
CROSS JOIN input j 
CROSS JOIN input k
WHERE i.value + j.value + k.value = 2020