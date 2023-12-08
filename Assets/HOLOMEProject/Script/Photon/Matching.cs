using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // �v���C���[���g�̖��O��"Player"�ɐݒ肷��
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
        Debug.Log("�T�[�o�[�ɐڑ����܂����B�B");
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // HoloLens�̃J�����̈ʒu���擾
        Transform mainCameraTransform = Camera.main.transform;


        if (mainCameraTransform != null)
        {
            // Main Camera �̈ʒu���擾
            Vector3 cameraPosition = mainCameraTransform.position;

            // 1���[�g���ȓ��̃����_���Ȉʒu�𐶐�
            Vector3 randomOffset = Random.onUnitSphere * 1f;

            // ��������ʒu���v�Z
            Vector3 position = cameraPosition + randomOffset;

            // �I�u�W�F�N�g�𐶐�
            //TODO: �o�b�N�G���h�ł���܂ŃL���������Œ�l�Ԃ�����
            Quaternion rotation = Quaternion.Euler(0, 270, 0);
            PhotonNetwork.Instantiate("MiiVerGhost", position, rotation);
            GameObject.Find("MiiVerGhost(Clone)").transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            Debug.LogError("���C���J����������܂���");
        }

        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        //var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        
        Debug.Log("���[���ɎQ�����܂����B");
    }
}