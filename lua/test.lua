local a = [[Battery 0: Not charging, 99%
Battery 0: design capacity 1953 mAh, last full capacity 1626 mAh = 83%
Battery 1: Discharging, 0%, rate information unavailable
Battery 2: Charging, 65%, 00:39:01 until charged
Battery 2: design capacity 5732 mAh, last full capacity 4900 mAh = 85%]]

function dump(o)
   if type(o) == 'table' then
      local s = '{ '
      for k,v in pairs(o) do
         if type(k) ~= 'number' then k = '"'..k..'"' end
         s = s .. '['..k..'] = ' .. dump(v) .. ','
      end
      return s .. '} '
   else
      return tostring(o)
   end
end

local battery_info = {}
local capacities = {}
for s in a:gmatch("[^\r\n]+") do
    local status, charge_str, _ = string.match(s, '.+: ([%a%s]+), (%d?%d?%d)%%,?(.*)')
    if status ~= nil  and tonumber(charge_str) ~= 0 then
        table.insert(battery_info, {status = status, charge = tonumber(charge_str)})
    else
        local cap_str = string.match(s, '.+:.+last full capacity (%d+)')
        table.insert(capacities, tonumber(cap_str))
    end
end
local capacity = 0
for _, cap in ipairs(capacities) do
    capacity = capacity + cap
end

local charge = 0
local status = "Not Charging"
for i, batt in ipairs(battery_info) do
    if capacities[i] ~= nil then
        if batt.status == "Charging" then
            status = batt.status  -- if one battery is charging we are charging
        end

        charge = charge + batt.charge * capacities[i]
    end
end
print(dump(battery_info))
print(dump(capacities))
print("before " .. charge)
print("before " .. capacity)
charge = charge / capacity

print(charge)
print(string.format('%d%%', math.floor(charge)))

local batteryType = ""
if (charge >= 1 and charge < 15) then
elseif (charge >= 15 and charge < 40) then batteryType = "battery-caution%s-symbolic"
elseif (charge >= 40 and charge < 60) then batteryType = "battery-low%s-symbolic"
elseif (charge >= 60 and charge < 80) then batteryType = "battery-good%s-symbolic"
elseif (charge >= 80 and charge <= 100) then batteryType = "battery-full%s-symbolic"
end

if status == 'Charging' then
    batteryType = string.format(batteryType, '-charging')
else
    batteryType = string.format(batteryType, '')
end

print(batteryType .. ".svg")
