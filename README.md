# SiguriaETeDhenave_GR21
1.Udhezimet per ekzekutimin e programit:

Ashtu siq eshte kerkuar ekzekutimi i programit behet duke shenuar sintaksat perkatese.
Per komanden frequency perdorim sintaksen: ds frequency <text> , psh si ne prove(screeenshot): ds frequency "Pershendetje nga FIEK!",
ku njekohesisht do te paraqitet edhe bar-grafi i kerkuar per pike shtese.
Per komanden beale kemi sintaksen: ds beale encrypt <book> <plaintext>, psh ds beale encrypt libri.txt "pershendetje" dhe
				   ds beale decrypt <book> <chipertext>, psh ds beale decrypt libri.txt "256 3 4 10 125 3 37 63 3 11 2 3".
Per komanden playfair kemi sintaksen: ds playfair encrypt <key> <plaintext>, psh ds playfair encrypt siguria "takohemi neser", 
				      ds playfair decrypt <key> <ciphertext>, psh ds playfair decrypt siguria "ne hp mb hr ta ra iz" dhe 
per kerkesen per pik shtese per te paraqitur tabelen kemi: ds playfair table <key>, psh ds playfair table siguira.


2.Pershkrim i shkurter per secilen komande.
	
                                  Komanda Frequensy


Kjo komande numeron se sa here paraqiten karakteret ose simbolet ne nje tekst.
Kemi zhvilluar nje program ne c#i cili numeron karakteret dhe simbolet(duke mos i 
perfshire hapsirat)dhe tregon se sa here jane paraqitur ne tekstin e dhene.
Simbolet dhe karakteret ne baze te paraqitjes te rradhiten prej atyre qe paraqiten
me shume deri tek ato qe paraqiten me pak.Gjithashtu i kemi paraqitur edhe me ane
te perqindjes(%). E gjithe kjo mundesohet duke krijuar nje array ku ruhen se sa 
here eshte paraqitur secili simbol, gjithashtu krijohet ni kopje e tij. Pastaj 
shiqojme se cili karakter eshte paraqitur me se shumti dhe e vendosim ne nje 
array poziten ku ndodhet aj dhe behet 0 ne array-in kopje duke vazhduar derisa te
behen te gjitha 0. Pastaj varesisht nga numri i simboleve shiqojm me rradhe ne array
ku jane to renditur cili simbol eshte paraqitur me se shpeshi dhe tek array i simboleve
marrim se sa her eshte paraqitur aj simbol. Kemi perdorur 33 pasi qe simbolet ne ASCII
code fillojn nga nr 33 prandaj jan bere mbledhjet(zbritjet) me 33.
E kemi bere edhe pjesen per pike shtese ne te cilen eshte kerkuar qe te paraqitet 
nje ASCII bar-graf qe vizualizon perqindjet e simbolece me ane te (#).
 


               



                        Komanda Beale 



Kjo komande bene zevendesimin e seciles shkronje te tekstit me poziten e asaj shkronje ne nje liber.
Tek beale enkriptimi(bEncrypt) behet duke marr secilen shkronje te plaintext dhe duke shiquar se ku eshte ajo ne
librin e dhene ku krijojme array-in me integera ku vendoset pozita e asaj shkronje ne librin e dhene.
Dekriptimi(bDecrypt) behet duke shikuar pozitat qe pranojme nga ciphertexti se cila shkronje eshte ajo.





                        Komanda Fairplay

Tek kjo komande kemi perdorur nje tabele 5x5 me emrin matricaCeles.

Nenkomanda fpEncrypt
 Enkriptimi te fairplay ben qe plaintexti te ndahet ne grupe prej dy shkronjave 
dhe nese nje shkronje mbetet ne fund e vetme ti shtohet shkronja X
Dhe te behet zevendesimi i shkronjes I me shkronjen J
pastaj behet krahasimi i shkronjave  ne tabele ku kemi 4 raste:
1.nese shkronja e pare eshte e njejte me shkronjen e dyte shtojme nje 
X ndermjet tyre ,enkriptojme qiftin e ri te shkronjave dhe i ribashkojme perseri shkronjat
perseritese dhe vazhdojme
2.nese shkronjat shfaqen ne te njejtin rresht te tabeles i zevendesojme ato me shkronja ne te djathten
e tyre perkatese ose nese jane ne fund te matries kalojme ne anene e majte te tabeles nese eshte e nevojshme.
3.nese shkronjat shfaqen ne te njejten kolone te tabeles ather duhet ti zevendesojme ato me shkronjat me poshte
ose duke kaluar ne maje te tabeles nese ato gjenden ne fund 
4.nese shkronjat jane ne rreshta dhe kolona te ndryshme i zevendesojme ato me shkronja ne te njejtin rresht perkates 
por ne palen tjeter te qosheve. Renditja eshte e rendesishme dmth shkronja e pare e pales duhet te zevendesohet 
se pari.


Nenkomanda fpDecrypt
Dekriptimi eshte e kunderta e enkriptimit.Ciphertextin e dekriptojme duke shikuar tabelen e fjaleve te enkriptuara dhe 
duke i zevendesuar ato me ane te rregullave perveq rregulles se pare rregullen e dyte dhe te trete e bejme duke zevendesuar 
lart dhe majtas ne vend te poshte dhe djathtas.

Gjenerimi i qelesit
Behet duke gjeneruar matricen Celes.


Per kete komande jemi bazuar ne pjesen qe e kemi bere gjate ushtrimeve me profesorin.

3. Rezultatet e ekzekutimit me nga nje shembull per secilen komande dhe nenkomande.
Screenshot-at jane te vendosur ne folderin rezultatet.

Detyra 2.

1.Udhezimet per ekzekutimin e programit:
Per komanden Create perdorim sintaksen:	ds create-user <name>,
Per komanden Delete perdorim sintaksen: ds delete-user <name>,
Per komanden ExportKey perdorim sintaksen: ds export-key <public|private> <name> [file].
Per komanden ImportKey perdorim sintaksen: ds import-key <name> <path>.
Kemi edhe nenkomanden DoesExists e cila perdoret per te shikuar se a ekziston useri.

2.Pershkrim i shkurter per secilen komande.

Komanda Create:
Tek kjo komand krijojm per secilin user nga 2 file user.xml dhe user.pub.xml 
te cilet permbajn qelesin privat respektivisht qelesin publik te userit me RSA algoritem.
Perndryshe nese useri ekziston atehere kthehet nje gabim.

Komanda Delete:
Kjo komand mundeson fshirjen e usereve. Varesisht se nese useri ka vetem qelesin privat ose publik ose te dy
programi automatikisht fshin dhe tregon se cilet ka fshir, ne qofte se nuk ekziston ai user kthen mesazh
gabimi.

Komanda ExportKey:
Kjo komand merr qeles privat ose publik te ndonje useri qe e kerkojm dhe e vendos ne nje file tjeter
varesisht se cilin qeles e kemi kerkuar. Ne qofte se useri nuk ekziston programi automatikisht 
e detekton mungesen e tij dhe lajmron se nuk mun te behet.

Komanda ImportKey:
Kjo komand e merr qelesin nga nje file dhe automatikisht e gjen se a eshte qeles privat apo publik.
Ne qofte se eshte qeles privat aj krijon edhe qelesin privat edhe publik per ate user kurse nese
eshte publik ruan vetem qelesin publik.


3. Rezultatet e ekzekutimit me nga nje shembull per secilen komande dhe nenkomande.

Screenshot-at jane te vendosur ne folderin rezultatet.
