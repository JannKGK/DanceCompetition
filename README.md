# Hambotantsuvõistlus

* Hambotantsuvõistlusel tantsitakse piki tänavat. Sama paari hindab žürii iga liige oma lõigul.

* Loo SQL lause tantsupaari sisestamiseks. Loo lause esimeses punktis hindamata paaride näitamiseks. 
  Loo lause määratud id-ga paarile hinde määramiseks.

* Loo veebileht tantsupaari lisamiseks. 
  Loo veebileht paaride näitamiseks, kes pole veel esimest hinnet saanud. 
  Hindaja saab paarile hinde andmiseks vajutada sobivat tema taga olevat numbrit ühest viieni.

* Loo sarnased lehed teiste hindamislõikude tarbeks. 
  Loo koondleht võistluse lõpetanud paaride tulemuste vaatamiseks, 
  kus on näha ka iga paari keskmine hinne.

### User guide: 
https://docs.google.com/document/d/1PRT-TL-ybt4JRZcL2y9EcRfgZ7dDNv0RQKXyGeKyB10/edit?usp=sharing

### SQL laused
Loo SQL lause tantsupaari sisestamiseks.

  <sub>INSERT INTO dbo.DancePair VALUES(1,'Bob and Mary',0,0,0);</sub>

Loo lause esimeses punktis hindamata paaride näitamiseks. 
  
  <sub>SELECT * FROM dbo.DancePair WHERE grade1 = 0;</sub>

Loo lause määratud id-ga paarile hinde määramiseks.

<sub>
  UPDATE dbo.DancePair
  SET grade1 = 4
  WHERE id = 1;
</sub>
