local FishermansFriend_Frame;
local FishermansFriend_EventFrame;

local FishermansFriend_PlayerTextures = {};
local FishermansFriend_AuraTextures = {};
local FishermansFriend_AbilityTextures = {};

local FishermansFriend_CastingTexture;
local FishermansFriend_CombatTexture;
local FishermansFriend_FollowingTexture;
local FishermansFriend_PlayerManaTexture;

local FishermansFriend_AutoAttackTexture;

local RangeIndicators = {};
Cooldowns = {};

local AuraList = {};

local FishermansFriend_UpdateTimer = 0;

function MakeAuraList()
	AuraList["Power Word: Fortitude"] = 1;
	AuraList["Renew"] = 2;
	AuraList["Power Word: Shield"] = 4;
	AuraList["Weakened Soul"] = 8;
	AuraList["Drink"] = 16;
end

function MakeFrame(xpos, ypos, r, g, b, a)
	local TMP_FRAME = CreateFrame("FRAME", nil, UIParent);
	TMP_FRAME:SetFrameStrata("FULLSCREEN_DIALOG");
	TMP_FRAME:SetWidth(5);
	TMP_FRAME:SetHeight(5);
	
	local TMP_TEXTURE = TMP_FRAME:CreateTexture(nil,"FULLSCREEN_DIALOG");
	TMP_TEXTURE:SetColorTexture(r,g,b,a);
	TMP_TEXTURE:SetAllPoints(TMP_FRAME);
	TMP_FRAME.texture = TMP_TEXTURE;
	
	TMP_FRAME:SetPoint("BOTTOMLEFT", xpos, ypos);
	TMP_FRAME:Show();
	return TMP_TEXTURE;
end


function FishermansFriend_OnLoad()
	print("FISHERMANS FRIEND LOADED");
	
	FishermansFriend_Frame = CreateFrame("Frame", nil);

	for i=0, 4 do
		FishermansFriend_PlayerTextures[i] = MakeFrame(i*5, 0, 0, 0, 0, 1);
		FishermansFriend_AuraTextures[i] = MakeFrame(i*5, 5, 0, 0, 0, 1);
	end

	for i=0, 4 do
		FishermansFriend_AbilityTextures[i] = MakeFrame(i*5, 10, 0, 0, 0, 1);
	end

	FishermansFriend_CastingTexture = MakeFrame(0, 15, 0, 1, 0, 1);
	FishermansFriend_CombatTexture = MakeFrame(5, 15, 0, 1, 0, 1);
	FishermansFriend_FollowingTexture = MakeFrame(10, 15, 0, 1, 0, 1);
	FishermansFriend_PlayerManaTexture = MakeFrame(15, 15, 0, 0, 0, 1);
	FishermansFriend_AutoAttackTexture = MakeFrame(20, 15, 0, 0, 0, 1);

	MakeAuraList();

	FishermansFriend_EventFrame = CreateFrame("FRAME", "FishermansFriend_EventFrame");
	FishermansFriend_EventFrame:SetScript("OnEvent", FishermansFriend_EventHandler);
	FishermansFriend_EventFrame:RegisterEvent("UI_ERROR_MESSAGE"); --arg1 ID?, arg2 message
	FishermansFriend_EventFrame:RegisterEvent("UNIT_SPELLCAST_START"); --arg1 castby, arg2 spellcast GUID, arg3 spellID
	FishermansFriend_EventFrame:RegisterEvent("UNIT_SPELLCAST_STOP"); --arg1 castby, arg2 spellcast GUID, arg3 spellID
	FishermansFriend_EventFrame:RegisterEvent("AUTOFOLLOW_BEGIN"); --arg1 unitID
	FishermansFriend_EventFrame:RegisterEvent("AUTOFOLLOW_END"); --no args
	FishermansFriend_EventFrame:RegisterEvent("PLAYER_REGEN_DISABLED"); --no args
	FishermansFriend_EventFrame:RegisterEvent("PLAYER_REGEN_ENABLED"); --no args
	FishermansFriend_EventFrame:RegisterEvent("UNIT_HEALTH"); --arg1 unitID
	FishermansFriend_EventFrame:RegisterEvent("UNIT_POWER_UPDATE"); --arg1 unitID, arg2 powerType (i.e. mana)
	FishermansFriend_EventFrame:RegisterEvent("UNIT_AURA"); --arg1 unitID
	
end

function ActionButton_UpdateRangeIndicator(self, checksRange, inRange)
	if ( self.HotKey:GetText() == RANGE_INDICATOR ) then
		if ( checksRange ) then
			self.HotKey:Show();
			if ( inRange ) then
				self.HotKey:SetVertexColor(LIGHTGRAY_FONT_COLOR:GetRGB());
			else
				self.HotKey:SetVertexColor(RED_FONT_COLOR:GetRGB());
			end
		else
			self.HotKey:Hide();
		end
	else
		if ( checksRange and not inRange ) then
			self.HotKey:SetVertexColor(RED_FONT_COLOR:GetRGB());
			RangeIndicators[self:GetName()] = 0;
		else
			self.HotKey:SetVertexColor(LIGHTGRAY_FONT_COLOR:GetRGB());
			RangeIndicators[self:GetName()] = 1;
		end
	end
end

function FishermansFriend_EventHandler(self, event, ...)
	--self, event, arg1, arg2, ... are all globals
	local arg1, arg2, arg3 = ...;
	local r, g, b, mana, manacur, hp, hpcur, ind;
	ind = nil;
	
	if event == "UI_ERROR_MESSAGE" then --arg1 ID?, arg2 message

	elseif event == "UNIT_SPELLCAST_START" then --arg1 castby, arg2 spellcast GUID, arg3 spellID
		FishermansFriend_CastingTexture:SetColorTexture(1,0,0,1);
	elseif event == "UNIT_SPELLCAST_STOP" then --arg1 castby, arg2 spellcast GUID, arg3 spellID
		FishermansFriend_CastingTexture:SetColorTexture(0,1,0,1);
	elseif event == "AUTOFOLLOW_BEGIN" then --arg1 unitID
		FishermansFriend_FollowingTexture:SetColorTexture(1,0,0,1);
	elseif event == "AUTOFOLLOW_END" then --no args
		FishermansFriend_FollowingTexture:SetColorTexture(0,1,0,1);
	elseif event == "PLAYER_REGEN_DISABLED" then --no args
		FishermansFriend_CombatTexture:SetColorTexture(1,0,0,1);
	elseif event == "PLAYER_REGEN_ENABLED" then --no args
		FishermansFriend_CombatTexture:SetColorTexture(0,1,0,1);
	elseif event == "UNIT_HEALTH" then --arg1 unitID
		--make this better, find raid/party and index or something
		if arg1 == "player" then
			ind = 0;
		elseif arg1 == "party1" then
			ind = 1;
		elseif arg1 == "party2" then
			ind = 2;
		elseif arg1 == "party3" then
			ind = 3;
		elseif arg1 == "party4" then
			ind = 4;
		end

		if ind ~= nil then
			hp = UnitHealthMax(arg1);
			hpcur = UnitHealth(arg1);
			
			r = bit.band(hp,255);
			g = bit.band(bit.rshift(hp,8),255);
			b = ceil((hpcur/hp)*100);
			FishermansFriend_PlayerTextures[ind]:SetColorTexture(r/255,g/255,b/255,1);
		end
	elseif event == "UNIT_POWER_UPDATE" then --arg1 unitID, arg2 powerType (i.e. mana)
		mana = UnitPowerMax("player");
		manacur = UnitPower("player");
		
		r = bit.band(mana,255);
		g = bit.band(bit.rshift(mana,8),255);
		b = ceil((manacur/mana)*100);
		FishermansFriend_PlayerManaTexture:SetColorTexture(r/255,g/255,b/255,1);
	elseif event == "UNIT_AURA" then --arg1 unitID
		--make this better, find raid/party and index or something
		if arg1 == "player" then
			ind = 0;
		elseif arg1 == "party1" then
			ind = 1;
		elseif arg1 == "party2" then
			ind = 2;
		elseif arg1 == "party3" then
			ind = 3;
		elseif arg1 == "party4" then
			ind = 4;
		end

		if ind ~= nil then
			r = 0;
			for i=1, 40 do
				local aura = UnitAura(arg1, i);
				if aura == nil then
					break;
				elseif AuraList[aura] ~= nil then
					r = r + AuraList[aura];
				end
			end
			for i=1, 40 do
				local aura = UnitDebuff(arg1, i);
				if aura == nil then
					break;
				elseif AuraList[aura] ~= nil then
					r = r + AuraList[aura];
				end
			end
			FishermansFriend_AuraTextures[ind]:SetColorTexture(r/255,0,0,1);
		end

	end
end

curdur = 0;

function FishermansFriend_OnUpdate()

	local r, g, b, mana, manacur, hp, hpcur, ind;

	--update ablity info
	for i=0, 4 do
		local buttonStr = "ActionButton"..(i+1);
		r = 1;
		g = 1;
		if RangeIndicators[buttonStr] == 1 then
			r = 0;
		end
		
		--for some reason the timer is out by a second
		if _G[buttonStr].cooldown:GetCooldownDuration() > 1000 then
			g = 0;
		end

		FishermansFriend_AbilityTextures[i]:SetColorTexture(r,g,0,1);
	end

	if ActionButton6:GetChecked() then
		FishermansFriend_AutoAttackTexture:SetColorTexture(0, 1, 0, 1);
	else
		FishermansFriend_AutoAttackTexture:SetColorTexture(0, 0, 0, 1);
	end
	

	--force update HP & auras on a timer
	if time()-FishermansFriend_UpdateTimer > 5 then

		--update player mana
		mana = UnitPowerMax("player");
		manacur = UnitPower("player");
		
		r = bit.band(mana,255);
		g = bit.band(bit.rshift(mana,8),255);
		b = ceil((manacur/mana)*100);
		FishermansFriend_PlayerManaTexture:SetColorTexture(r/255,g/255,b/255,1);

		--update party info
		for i=0, 4 do
			local unitStr = "party"..i;

			if i == 0 then
				unitStr = "player";
			end	

			if UnitClass(unitStr) ~= nil then

				--update health
				hp = UnitHealthMax(unitStr);
				hpcur = UnitHealth(unitStr);
				
				r = bit.band(hp,255);
				g = bit.band(bit.rshift(hp,8),255);
				b = ceil((hpcur/hp)*100);
				FishermansFriend_PlayerTextures[i]:SetColorTexture(r/255,g/255,b/255,1);

				--update buffs/debuffs
				if ind ~= nil then
					r = 0;
					for j=1, 40 do
						local aura = UnitAura(unitStr, j);
						if aura == nil then
							break;
						elseif AuraList[aura] ~= nil then
							r = r + AuraList[aura];
						end
					end
					local b = 0;
					for j=1, 40 do
						local aura, _, _, strType = UnitDebuff(unitStr, j);
						if aura == nil then
							break;
						end

						if strType == "Magic" then
							b = 1;
						end

						if AuraList[aura] ~= nil then
							r = r + AuraList[aura];
						end
					end
					FishermansFriend_AuraTextures[i]:SetColorTexture(r/255,0,b,1);
				end
			else
				FishermansFriend_PlayerTextures[i]:SetColorTexture(0,0,0,1);
			end
		end
		FishermansFriend_UpdateTimer = time();
	end	


end