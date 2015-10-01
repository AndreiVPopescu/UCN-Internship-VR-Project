//#if ENABLE_UNET

namespace UnityEngine.Networking
{
    using UnityEngine.UI;
    using System.Collections.Generic;
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class NetworkManagerHUD : MonoBehaviour
	{
		public NetworkManager manager;
        public Button startHostButton;
        public Button startButton;
        public Button joinButton;
        public Button startMatchMakerButton;
        public Button createInternetMatchButton;
        public Button findInternetMatchButton;
        public Button returnStartButton;
        public Transform canvasPanelTransform;
        private Match.MatchDesc[] test= new Match.MatchDesc[100];
        private bool ok = true;
        public Sprite image;
        private List<GameObject> mmButtons = new List<GameObject>();
        public GameObject playerToDelete;
        public bool isHost;
        [SerializeField] public bool showGUI = true;
		[SerializeField] public int offsetX;
		[SerializeField] public int offsetY;
        

		// Runtime variable
		bool showServer = false;

		void Awake()
		{
			manager = GetComponent<NetworkManager>();
            DontDestroyOnLoad(transform.gameObject);
        }

		void Update()
		{
			if (!showGUI)
				return;
			if (NetworkServer.active && NetworkClient.active)
			{
				if (Input.GetKeyDown(KeyCode.X))
				{
					manager.StopHost();
                    GameObject.Destroy(gameObject);
                }
			}
		}

        public GameObject CreateButton(Vector3 position, Vector2 size, Match.MatchDesc match, string text)
        {
            GameObject button = new GameObject();
            button.transform.parent = canvasPanelTransform;
            button.AddComponent<RectTransform>();
            button.AddComponent<Button>();
            button.AddComponent<Image>();
            button.GetComponent<Image>().sprite = image;
            button.AddComponent<BoxCollider>();
            button.GetComponent<BoxCollider>().size = new Vector3((float)17.25,(float)12.89,1);
            button.transform.localScale = new Vector3((float)0.3,(float)0.3,(float)0.3);
            GameObject textB = new GameObject();
            textB.transform.parent = button.transform;
            textB.AddComponent<Text>();
            textB.transform.localScale = new Vector2((float)0.2,(float)0.2);
            Text buttonText = textB.GetComponent<Text>();
            buttonText.text = "Join Match "+text;
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.font= (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            buttonText.fontSize = 14;
            buttonText.color = Color.black;
            button.transform.localPosition = position;
            button.GetComponent<RectTransform>().sizeDelta = size;
            button.GetComponent<Button>().onClick.AddListener(delegate { joinMatch(match); });
            return button;
        }

        public void joinGame()
        {
            joinButton.gameObject.SetActive(false);
            startHostButton.gameObject.SetActive(false);
            startMatchMakerButton.gameObject.SetActive(false);
            manager.StartClient();
            ok = true;
        }

        public void startHost()
        {
            
            manager.StartHost();
            startHostButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            startMatchMakerButton.gameObject.SetActive(false);
            ok = true;
            isHost = true;
        }

        public void startClient()
        {
            manager.StartClient();
            ok = true;
            isHost = false;
        }

        public void startServer()
        {
            manager.StartServer();
        }

        public void startMatchMaker()
        {
            manager.StartMatchMaker();
            startMatchMakerButton.gameObject.SetActive(false);
            startHostButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            ok = false;
            returnStartButton.gameObject.SetActive(true);
        }

        public void returnStart()
        {
            returnStartButton.gameObject.SetActive(false);
            manager.StopMatchMaker();
            startMatchMakerButton.gameObject.SetActive(true);
            startHostButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            findInternetMatchButton.gameObject.SetActive(false);
            createInternetMatchButton.gameObject.SetActive(false);
        }

        public void createInternetMatch()
        {
            manager.matchMaker.CreateMatch(System.Environment.MachineName, manager.matchSize, true, "", manager.OnMatchCreate);
            createInternetMatchButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            startHostButton.gameObject.SetActive(false);
            findInternetMatchButton.gameObject.SetActive(false);
            ok = true;
            isHost = true;
        }

        public void findInternetMatch()
        {
            manager.matchMaker.ListMatches(0, 20, "", manager.OnMatchList);
            findInternetMatchButton.gameObject.SetActive(false);
            createInternetMatchButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            startHostButton.gameObject.SetActive(false);
        }

        public void joinMatch(Match.MatchDesc match)
        {
            manager.matchName = match.name;
            manager.matchSize = (uint)match.currentSize;
            manager.matchMaker.JoinMatch(match.networkId, "", manager.OnMatchJoined);
            isHost = false;
        }

        public void ready()
        {
            GameObject.Destroy(playerToDelete);
            //Application.LoadLevel("Main");
            ClientScene.Ready(manager.client.connection);
            if (ClientScene.localPlayers.Count == 0)
            {
                ClientScene.AddPlayer(0);
            }
            startButton.gameObject.SetActive(false);
        }
        
		void OnGUI()
		{
			if (!showGUI)
				return;

			int xpos = 10 + offsetX;
			int ypos = 40 + offsetY;
			int spacing = 24;

			if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
			{
                if (startHostButton!=null)
                startHostButton.gameObject.SetActive(true);
                if (joinButton!=null)
                joinButton.gameObject.SetActive(true);

				//manager.networkAddress = GUI.TextField(new Rect(xpos + 100, ypos, 95, 20), manager.networkAddress);
				ypos += spacing;

				if (GUI.Button(new Rect(xpos, ypos, 200, 20), "LAN Server Only(S)"))
				{
					manager.StartServer();
				}
				ypos += spacing;
			}
			else
			{
				if (NetworkServer.active)
				{
					GUI.Label(new Rect(xpos, ypos, 300, 20), "Server: port=" + manager.networkPort);
					ypos += spacing;
				}
				if (NetworkClient.active)
				{
					GUI.Label(new Rect(xpos, ypos, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
					ypos += spacing;
				}
			}

			if (NetworkClient.active && !ClientScene.ready)
			{
                if (startButton!=null)
                startButton.gameObject.SetActive(true);
				ypos += spacing;
			}

			if (NetworkServer.active || NetworkClient.active)
			{
				if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Stop (X)"))
				{
					manager.StopHost();
                    GameObject.Destroy(gameObject);
				}
				ypos += spacing;
                if (Application.loadedLevel==0)
                foreach (GameObject obj in mmButtons)
                {
                    obj.SetActive(false);
                }
			}
            // MATCHMAKER
			if (!NetworkServer.active && !NetworkClient.active)
			{
				ypos += 10;

				if (manager.matchMaker == null)
				{
                    if (startMatchMakerButton!=null)
                    startMatchMakerButton.gameObject.SetActive(true);
				}
				else
				{
					if (manager.matchInfo == null)
					{
                        if (manager.matches == null)
                        {
                            if (!ok && createInternetMatchButton!=null)
                            {
                                createInternetMatchButton.gameObject.SetActive(true);
                            }
                            
                            ypos += spacing;
                            ypos += 10;
                            if (!ok && findInternetMatchButton!=null)
                            {
                                findInternetMatchButton.gameObject.SetActive(true);
                                ok = true;
                            }
                        }
                        else
                        {
                            if (test[0] == null)
                            {
                                int i = 0;
                                test = new Match.MatchDesc[100];
                                foreach (var match in manager.matches)
                                {
                                    test[i] = match;
                                    if (!(i > 0 && test[i].networkId == test[i - 1].networkId)) { i++; }
                                }
                                for (int j = 0; j < i; j++)
                                {
                                    mmButtons.Add(CreateButton(new Vector3(20, (float)4.5 - j, (float)-157.8), new Vector2(20, 15), test[j], test[j].name));
                                    print(test[j].networkId);
                                }
                            }
                        }
					}

					if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Change MM server"))
					{
						showServer = !showServer;
					}
					if (showServer)
					{
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Local"))
						{
							manager.SetMatchHost("localhost", 1337, false);
							showServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Internet"))
						{
							manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
							showServer = false;
						}
						ypos += spacing;
						if (GUI.Button(new Rect(xpos, ypos, 100, 20), "Staging"))
						{
							manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
							showServer = false;
						}
					}
					ypos += spacing;

					GUI.Label(new Rect(xpos, ypos, 300, 20), "MM Uri: " + manager.matchMaker.baseUri);
					ypos += spacing;
				}
			}
		}
	}
};
//#endif //ENABLE_UNET
