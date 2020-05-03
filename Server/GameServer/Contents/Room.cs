using AutoMapper;
using GameServer.Extensions;
using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Extensions;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GameServer.Contents
{
    public enum RoomState
    {
        Ready,
        Loading,
        Play,
        Result,
        End
    }

    public interface IRoom
    {
        Action<string> OnGameEnd { get; set; }

        void Release();

        bool Enter(RoomUser user);
        bool Exit(string id);

        string GroupName();
        int UserCount();
        GameUser GetUserById(string id);
        GameUser GetUserByConnectionId(string connectionId);

        void CSID_CompleteLoading(string id);
        void CSID_CreateCube(CS_CreateCube cs);
        void CSID_MoveCube(CS_MoveCube cs);
        void CSID_CombineCube(CS_CombineCube cs);
        void CSID_DeleteCube(CS_DeleteCube cs);
        void CSID_ShotMissile(CS_ShotMissile cs);
        void CSID_DieMonster(CS_DieMonster cs);
        void CSID_EscapeMonster(CS_EscapeMonster cs);
        void CSID_UpdateSlot(CS_UpdateSlot cs);
    }

    public class Room : IRoom
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IMapper _mapper;
        private readonly string _groupName;
        private RoomState _state;
        private readonly List<GameUser> _users = new List<GameUser>();

        private readonly Timer _timer = new Timer();

        public Action<string> OnGameEnd { get; set; }

        public Room(IHubContext<GameHub> context, IMapper mapper, string groupName)
        {
            _context = context;
            _mapper = mapper;
            _groupName = groupName;

            _timer = new Timer
            {
                Interval = 100,
                Enabled = true,
            };

            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            Reset();
        }

        public void Release()
        {
            _context.Clients(_users).SendCoreAsync("DeleteRoom", PayloadPack.Success(new SC_DeleteRoom()
            {

            }));

            _state = RoomState.End;
            _timer.Stop();
            _users.Clear();
        }

        private void Reset()
        {
            _state = RoomState.Ready;
        }

        private void Result()
        {
            _state = RoomState.Result;

            var users = _users.OrderBy(x => x.Life).ToList();

            users.ForEach((x, i) =>
            {
                x.Rank = i;
                x.Money = ServerDefine.Rank2Money(i);
            });

            _context.Clients(_users).SendCoreAsync("Result", PayloadPack.Success(new SC_Result()
            {
                Users = users,
            }));

            OnGameEnd?.Invoke(_groupName);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            switch (_state)
            {
                case RoomState.Ready:
                    {
                        if (_users.Count >= ServerDefine.MAX_GAME_USER)
                        {
                            Start();
                        }
                    }
                    break;
                case RoomState.Loading:
                    {

                    }
                    break;
            }
        }

        public bool Enter(RoomUser user)
        {
            if (_state != RoomState.Ready)
                return false;

            if (GetUserById(user.Id) != null)
                return false;

            var newUser = _mapper.Map<GameUser>(user);

            _users.Add(newUser);

            return true;
        }

        public bool Exit(string id)
        {
            var user = GetUserById(id);
            if (user == null)
                return false;

            _users.Remove(user);

            //한명이라도 나가면 게임 종료
            Result();

            return true;
        }

        private void Start()
        {
            Reset();

            SCID_Loading();
        }

        public string GroupName()
        {
            return _groupName;
        }

        public int UserCount()
        {
            return _users.Count;
        }

        public GameUser GetUserById(string id)
        {
            return _users.Find(p => p.Id == id);
        }

        public GameUser GetUserByConnectionId(string connectionId)
        {
            return _users.Find(p => p.ConnectionId == connectionId);
        }

        private void SetUserState(string id, UserState state)
        {
            var user = GetUserById(id);
            if (user == null)
                return;

            user.State = state;
        }

        private void SetUserState(UserState state)
        {
            _users.ForEach(x => x.State = state);
        }

        private bool CheckUserState(UserState state)
        {
            return _users.TrueForAll(x => x.State == state);
        }

        private void SCID_Loading()
        {
            _state = RoomState.Loading;

            _context.Clients(_users).SendCoreAsync("Loading", PayloadPack.Success(new SC_Loading()
            {
                Users = _users,
            }));
        }

        public void CSID_CompleteLoading(string id)
        {
            if (_state != RoomState.Loading)
            {   //플레이 중에 로딩 완료가 오면 재접속
                return;
            }

            SetUserState(id, UserState.CompleteLoading);

            SCID_Play();
        }

        private void SCID_Play()
        {
            if (!CheckUserState(UserState.CompleteLoading))
                return; //아직 전부 로딩이 안되었음

            SetUserState(UserState.Play);

            _context.Clients(_users).SendCoreAsync("Play", PayloadPack.Success(new SC_Play()
            {

            }));

            _state = RoomState.Play;

            SCID_Wave();
        }

        private void SCID_Wave()
        {
            _context.Clients(_users).SendCoreAsync("Wave", PayloadPack.Success(new SC_Wave()
            {

            }));
        }

        public void CSID_CreateCube(CS_CreateCube cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("CreateCube", PayloadPack.Success(new SC_CreateCube()
            {
                Id = cs.Id,
                NewCube = cs.NewCube,
            }));
        }

        public void CSID_MoveCube(CS_MoveCube cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("MoveCube", PayloadPack.Success(new SC_MoveCube()
            {
                Id = cs.Id,
                CubeSeq = cs.CubeSeq,
                PositionX = cs.PositionX,
                PositionY = cs.PositionY,
            }));
        }

        public void CSID_CombineCube(CS_CombineCube cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("CombineCube", PayloadPack.Success(new SC_CombineCube()
            {
                Id = cs.Id,
                OwnerSeq = cs.OwnerSeq,
                TargetSeq = cs.TargetSeq,
            }));
        }

        public void CSID_DeleteCube(CS_DeleteCube cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("DeleteCube", PayloadPack.Success(new SC_DeleteCube()
            {
                Id = cs.Id,
                DeleteCubes = cs.DeleteCubes,
            }));
        }

        public void CSID_ShotMissile(CS_ShotMissile cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("ShotMissile", PayloadPack.Success(new SC_ShotMissile()
            {
                Id = cs.Id,
                CubeSeq = cs.CubeSeq,
                MonsterSeq = cs.MonsterSeq,
            }));
        }

        public void CSID_DieMonster(CS_DieMonster cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("DieMonster", PayloadPack.Success(new SC_DieMonster()
            {
                Id = cs.Id,
                MonsterSeq = cs.MonsterSeq,
            }));
        }

        public void CSID_EscapeMonster(CS_EscapeMonster cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("EscapeMonster", PayloadPack.Success(new SC_EscapeMonster()
            {
                Id = cs.Id,
                MonsterSeq = cs.MonsterSeq,
            }));

            var user = GetUserById(cs.Id);
            if (user != null)
            {
                user.Life -= 1;

                if (user.Life <= 0)
                {   //게임 종료
                    //Result();
                }
            }
        }

        public void CSID_UpdateSlot(CS_UpdateSlot cs)
        {
            _context.ClientsExceptById(_users, cs.Id).SendCoreAsync("UpdateSlot", PayloadPack.Success(new SC_UpdateSlot()
            {
                Id = cs.Id,
                SlotIndex = cs.SlotIndex,
                SlotLv = cs.SlotLv,
            }));
        }
    }
}
