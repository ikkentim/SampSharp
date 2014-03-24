// Copyright (C) 2011-2013 Zeex
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#ifndef SAMPGDK_A_VEHICLES_HPP
#define SAMPGDK_A_VEHICLES_HPP

#include <cmath>
#include <string>

#include <sampgdk/a_vehicles.h>

SAMPGDK_BEGIN_NAMESPACE

class Vehicle {
 public:
  Vehicle(int vehicleid): id_(vehicleid) {}

  operator int() const { return id_; }

  int GetId() const { return id_; }

  static Vehicle Create(int type, float x, float y, float z, float rotation, int color1, int color2, int respawn_delay) {
    return CreateVehicle(type, x, y, z, rotation, color1, color2, respawn_delay);
  }

  bool IsValid() const
    { return IsValidVehicle(id_); }

  float GetDistanceFromPoint(float x, float y, float z) const
    { return GetVehicleDistanceFromPoint(id_, x, y, z); }
  bool Destroy() const 
    { return DestroyVehicle(id_); }
  bool IsStreamedInFor(int playerid) const
    { return IsVehicleStreamedIn(id_, playerid); }
  bool GetPos(float *x, float *y, float *z) const
    { return GetVehiclePos(id_, x, y, z); }
  bool GetPos(float &x, float &y, float &z) const
    { return GetVehiclePos(id_, &x, &y, &z); }
  bool SetPos(float x, float y, float z) const
    { return SetVehiclePos(id_, x, y, z); }
  bool GetZAngle(float *z_angle) const
    { return GetVehicleZAngle(id_, z_angle); }
  bool GetZAngle(float &z_angle) const
    { return GetVehicleZAngle(id_, &z_angle); }
  float GetZAngle() const { 
    float z_angle;
    GetVehicleZAngle(id_, &z_angle); 
    return z_angle;
  }
  bool GetRotationQuat(float *w, float *x, float *y, float *z) const
    { return GetVehicleRotationQuat(id_, w, x, y, z); }
  bool SetZAngle(float z_angle) const
    { return SetVehicleZAngle(id_, z_angle); }
  bool GetRotationQuat(float &w, float &x, float &y, float &z) const
    { return GetVehicleRotationQuat(id_, &w, &x, &y, &z); }
  bool SetParamsForPlayer(int playerid, bool objective, bool doorslocked) const
    { return SetVehicleParamsForPlayer(id_, playerid, objective, doorslocked); }
  bool SetParamsEx(bool engine, bool lights, bool alarm, bool doors, bool bonnet, bool boot, bool objective) const
    { return SetVehicleParamsEx(id_, engine, lights, alarm, doors, bonnet, boot, objective); }
  bool GetParamsEx(bool *engine, bool *lights, bool *alarm, bool *doors, bool *bonnet, bool *boot, bool *objective) const
    { return GetVehicleParamsEx(id_, engine, lights, alarm, doors, bonnet, boot, objective); }
  bool GetParamsEx(bool &engine, bool &lights, bool &alarm, bool &doors, bool &bonnet, bool &boot, bool &objective) const
    { return GetVehicleParamsEx(id_, &engine, &lights, &alarm, &doors, &bonnet, &boot, &objective); }
  bool SetToRespawn() const
    { return SetVehicleToRespawn(id_); }
  bool LinkToInterior(int interiorid) const
    { return LinkVehicleToInterior(id_, interiorid); }
  bool AddComponent(int componentid) const 
    { return AddVehicleComponent(id_, componentid); }
  bool RemoveComponent(int componentid) const
    { return RemoveVehicleComponent(id_, componentid); }
  bool ChangeColor(int color1, int color2) const
    { return ChangeVehicleColor(id_, color1, color2); }
  bool ChangePaintjob(int paintjobid) const
    { return ChangeVehiclePaintjob(id_, paintjobid); }
  bool SetHealth(float health) const
    { return SetVehicleHealth(id_, health); }
  bool GetHealth(float *health) const
    { return GetVehicleHealth(id_, health); }
  bool GetHealth(float &health) const
    { return GetVehicleHealth(id_, &health); }
  float GetHealth() const {
    float health;
    GetVehicleHealth(id_, &health);
    return health;
  }
  bool AttachTrailer(int trailerid) const
    { return AttachTrailerToVehicle(trailerid, id_); }
  bool DetachTrailer() const
    { return DetachTrailerFromVehicle(id_); }
  bool IsTrailerAttached() const
    { return IsTrailerAttachedToVehicle(id_); }
  int GetTrailer() const
    { return GetVehicleTrailer(id_); }
  bool SetNumberPlate(const char *numberplate) const
    { return SetVehicleNumberPlate(id_, numberplate); }
  int GetModel() const
    { return GetVehicleModel(id_); }
  int GetComponentInSlot(int slot) const
    { return GetVehicleComponentInSlot(id_, slot); }
  bool Repair() const
    { return RepairVehicle(id_); }
  bool GetVelocity(float *X, float *Y, float *Z) const
    { return GetVehicleVelocity(id_, X, Y, Z); }
  bool GetVelocity(float &X, float &Y, float &Z) const
    { return GetVehicleVelocity(id_, &X, &Y, &Z); }
  float GetSpeed() const {
    float velX, velY, velZ;
    GetVelocity(velX, velY, velZ);
    return std::sqrt(velX*velX + velY*velY + velZ*velZ);
  }
  bool SetVelocity(float X, float Y, float Z) const
    { return SetVehicleVelocity(id_, X, Y, Z); }
  bool SetAngularVelocity(float X, float Y, float Z) const
    { return SetVehicleAngularVelocity(id_, X, Y, Z); }
  bool GetDamageStatus(int *panels, int *doors, int *lights, int *tires) const
    { return GetVehicleDamageStatus(id_, panels, doors, lights, tires); }
  bool GetDamageStatus(int &panels, int &doors, int &lights, int &tires) const
    { return GetVehicleDamageStatus(id_, &panels, &doors, &lights, &tires); }
  bool UpdateDamageStatus(int panels, int doors, int lights, int tires) const
    { return UpdateVehicleDamageStatus(id_, panels, doors, lights, tires); }
  bool SetVirtualWorld(int worldid) const
    { return SetVehicleVirtualWorld(id_, worldid); }
  int GetVirtualWorld() const
    { return GetVehicleVirtualWorld(id_); }

 private:
  const int id_;
};

SAMPGDK_END_NAMESPACE

#endif // !SAMPGDK_A_VEHICLES_HPP
