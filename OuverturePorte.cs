using System.Collections;
using UnityEngine;

/** OuverturePorte
 * 
 * Permet d'ouvrir une porte en faisant bouger les deux parties de la porte.
 * 
 * Vous devrez modifier cette classe afin qu'elle s'ouvre si on clique dessus.
 * */
public class OuverturePorte : MonoBehaviour
{
    public bool _doitOuvrirDebut = false;

    private GameObject _partieGauche;
    private GameObject _partieDroite;

    private float _vitesseOuverture = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _partieGauche = transform.Find("Porte_Gauche").gameObject;
        _partieDroite = transform.Find("Porte_Droite").gameObject;

        if (_doitOuvrirDebut)
        {
            OuvrirPorte();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OuvrirPorte()
    {
        StartCoroutine("OuvrirPorteCoroutine");
    }

    private IEnumerator OuvrirPorteCoroutine()
    {
        while (_partieGauche.transform.localPosition.x >= -6.0f)
        {
            float deplacement = Time.deltaTime * _vitesseOuverture;
            _partieDroite.transform.localPosition += new Vector3(deplacement, 0, 0);
            _partieGauche.transform.localPosition += new Vector3(-deplacement, 0, 0);
            yield return null;
        }
    }
}
