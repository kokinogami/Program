using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BossState
{
    /// <summary>
    /// �X�e�[�g�̎��s���Ǘ�����N���X
    /// </summary>
    public class BossStateProcessor
    {
        //�X�e�[�g�{��
        private BossState _State;
        public BossState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }

    //�X�e�[�g�̃N���X
    public abstract class BossState
    {
        //�f���Q�[�g
        public Action ExecAction { get; set; }

        //���s����
        public virtual void Execute()
        {
            if (ExecAction != null) ExecAction();
        }

        //�X�e�[�g�����擾���郁�\�b�h
        public abstract string GetStateName();
    }

    //�ҋ@���
    public class BossStateIdle : BossState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }
    //�ړ����
    public class BossStateMove : BossState
    {
        public override string GetStateName()
        {
            return "State:Move";
        }
    }
    public class BossStateAttack1 : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack1";
        }
    }
    public class BossStateAttack1Idle : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack1Idle";
        }
    }

    public class BossStateAttack2 : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack2";
        }
    }
    public class BossStateAttack3 : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack3";
        }
    }

    public class BossStateAttack3Idle : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack3Idle";
        }
    }
    
    public class BossStateAttack4 : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack4";
        }
    }

    public class BossStateAttack4Idle : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack4Idle";
        }
    }

    public class BossStateAttack5 : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack5";
        }
    }

    public class BossStateAttack5Idle : BossState
    {
        public override string GetStateName()
        {
            return "State:Attack5Idle";
        }
    }
    
    public class BossStateChangeFloor : BossState
    {
        public override string GetStateName()
        {
            return "State:ChangeFloor";
        }
    }
}
