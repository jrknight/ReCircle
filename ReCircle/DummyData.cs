using ReCircle.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCircle
{
    public class DummyData
    {
        public static DummyData Instance { get; set; }

        public static void Setup()
        {
            Instance = new DummyData { BrowsePage = new List<Item> { new Item { Title = "Shoes", OwnerEmail="elijahtbouchard@gmail.com", OwnerNickname = "Elijah", Description = "Bought them a few months ago, didn't think I would out grow them so fast. Barely used.", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://lh3.googleusercontent.com/hjJnqe2QCCzP02vgB1T1kbRIXa9zRFe_bfhq31Md-Q360SvCGdnzBRkJBsgbpCV-TlMS7PC0MeKwiUuDWMsf_olc5FPhbuug5OlfCoOShbDAD7iLmaOOMhpwOVa7nypsuhg2gBZH-2I0hZ_ip-bAk1Hc3bKyTa9fRE5D2FJOaHpUkpwueRUME0cE5fe6NUv8qNj6lgIj0rEMwo6Tb-IxbJa6U-O0v5leMqPVQiZPsGTMmLcQ68oFr0Dy8cdJCbSAPLH0AOQxsMjhQ3VSpaLrDJDPL4-aqGsV8Lm82qN1GSFcfoEW5PUz9ralcOR6OafR3Pcoulq8A6Odw2gzDJOUNIijQ44lMLPyFU2-_xFkW36yl7xA64sHJ7UoCPvt8jRMI5q-f2F_YXewsItcwPU7GkUBZ46htdYw2-vPmIQO1KT4o-DeYZzp9G97934gTlWoyUWOb3RBRAMne9DtA1LoraGBEkzWnNxQ5kK3rKwr6WMH2huXlOnSm62avj-sJH6zHfuHZJK-ESM-wjPPHNdP3dvp0jiN8e5buHZkFHGVOgf64xcXGExuhiT2Rbr35LQUNCd4AZvXMW2kPI96ZZsSWquH7FD9bnhBOiLkvEc8gVrLDBjJPOYiWvjYqcyavx4=w988-h741-no") },
                new Item{ Title = "Calculus Book", OwnerEmail="coltonjjacobson@gmail.com", OwnerNickname= "Colton", Description = "Bought it for calculus last year, got an A. Don't need it anymore.", CurrentHolderNickname = "Colton", PhotoUrl = new Uri("https://lh3.googleusercontent.com/v7EQ7N3iosmXo07aYr9S-BShlBxgpPo2Wl5OYKSljoEzc6qdyv4RuRtFneOpLuJF5i7-r7c6Z9QgC2RxM76wf6ZeglE25zjAWNcVeYcQ5UXrvG-_C-wQ2Q79uWRLaQnAAvSIyCIFtCElX7U7rlBhBkOOZjfLf_i8q2cRW0t3NPAMbATbAwRdBe-bjnMcK9LvgfO2D7nvyqfSILNz5diboVxuZfuB5D58wyIbFplfObSfLlq4s7sxCtcBRojSbPK1t18qtJzZVoS0BDkACnmb02D_jJS34DTZLgYPXRp5LvnRO7ZeHYmtUaU3w59mFzEWvd4TCXTn4-3F8lwBapxto_zoULgupiFH9o-cQwqz638Zrf65b5b1M0jpW1Ufwn9xoc9eAkjb1orTTYpLtlRlOA6ty5t3Xlxi_7lJTNN1VrgcKkgtUVZvkxceZLlzyunN7os6AkHGXZEUJCdjfoMuQX3vqJtO3bGgeKuOaHJlbtV2hv-jvLXhEDn4k4dsFx3UY_3bvuZrFubMISYyXvWlzTHIIYP_qeMtLnmFaR0UL-zlPRqCtrKrYqz7cposHUzD6_B1KXBsb5cgyZxFOdx4g8ygDIjxQiSBiRjMGukWXSptqNRRNaiFBMkFkAhALhk=w556-h741-no") },
                new Item { Title = "College Ruled Paper", OwnerEmail="deniellegoliva@gmail.com", OwnerNickname = "Denielle", Description = "400 sheets of college ruled binder paper. 10.5in x 8in", CurrentHolderNickname = "Denielle", PhotoUrl = new Uri("https://lh3.googleusercontent.com/hwep8NA7tVQi15fzZIJl1wLDwLZeGk-WtqmTZaJrEwUYX9VW44YCSQgJ-O6TRXRp55Ocu5YqHcUWacUL9NzRVRp1lB_dkUEqh87nSJepuXes118oqvdqx4v0wsBNR6G3vtdoQ1OyZzAlBZE3-f9BTpR6RuASJVE0M-pvoWfY0_XRffVMeNaCD3apbL3H0bIlPxTSLgkVefksytlrcPeAk86gx1g-HgpLFm9ZoVVm5jluWsr3M2_0C9g58MSDH0bmsYfwaVXYbeIVE0VFBHyT1FftGYRNavYxGP56wnLpyMfkrHv4-5-vf5sfH8QTYMZeRz3LbFPfgX1J64Pqsps_tFCYfi4wp35oQsFImRSp0BcJ6DFsYdV_pzWzgDBcu5ezShBxU2X90H15yVbChH_RdLwZkNxgnyD9ADbqsECTAIZRAJRxZhBu0M3VPvQ4StLC4Upjv7pOA2nfjYvL9GJcrtS7eLYgwi8khnOd-KnRejJ_u8n2-YnfDpM8hQyKe_G48QDtdKY7htUYN6Vhkc4e2SGDyyd3i6bPl8FIyMk9hPU5Vh4Ml1LLq8T5KCXOFXd4ScKo_OG8bBvK4ZBJFpuwIhtMj7LuFX0SEK53YXsastLUVn6gN04m3kGJSaocePo=w556-h741-no") },
                new Item { Title = "Composition Book", OwnerEmail="joshrknight@gmail.com", OwnerNickname = "Josh", Description = "College ruled paper, only 1 page missing. Otherwise, perfectly new.", CurrentHolderNickname = "Josh", PhotoUrl = new Uri("https://lh3.googleusercontent.com/Fy-jLKpSdkIKrCuMf4yGexz5ivClKVcFjrF24SkHoNSAnjHFTjAfnEidVX2N6FMI9R_DA3d1HTaT4qEOtVmOuedkeCyAQ-qG8mPCqv4Z1Cc5a45PT3LIzS3LVlhSsI8uHex2hNr4U8xt_LUdOa2pGkS2Nx7JCXsgtfv1aYauE9vsJDyPGQDFDlk727fhUp6l_hNTe-TlzOTUF79yCwXUK1mD-zqvFpdoz36UB-uc2bd-Le1dHMY5csbwd3bY6DbxH18VEoWdrHcXZaWUng8AidhlF6BKV0IdIx6SjF2ttRxVNBH-y4qsF0nRRtGN5BxOR2FvZInJNSxt239Sv5695JGiBZiCRURudv4vCb3vdy2u_BUHroFznRuN88MMZsPcwC-C_AdioOqqX6HM9MkGvr9sf4Q4foh-D7QLJVISUo_wfPFP8dcdk6OcJkWRKF3340RWB14qU_3-uLVbq54v4PI5S9ylUSc0ggQuzl1aY0OxMs8dTkXhlOcdxDws7An81y5bxELAL79UfooZPrCrye9NF1TM9cCsxS9NvzLisDCY3g6LCWegmM1pt0vL5QD98bGs5WtQH2CVUf4StzVRBOxHbVMxos8IzBHj4VNgnrm9OVSeDq3hevTG-hwShBM=w556-h741-no") },
                new Item { Title = "Binders", OwnerEmail="deniellegoliva@gmail.com", OwnerNickname = "Denielle", Description = "Two 3 ring binders. One black, one white. Already been used by no tears. Has pockets on the inside!", CurrentHolderNickname = "Denielle", PhotoUrl = new Uri("https://lh3.googleusercontent.com/SP84gYRcgQP_KdLjU5_UuJyJVJCrzzr-t8GxWqhBn5gn-wD2mdwVBawaa7YnuFyVeAvg3UmAjiueGtfiX9pmqlHcqG5_dckzzldKD1oztmYt1ZHLlur-Dht2UdOu3JAKkKEGulJLUnC7tM2hfuj_g4EOGD3_3PLh9GanBwjOiAJiFA_mBXrnOjyfHJ6SrWCX6uQIqUhMTNc0M0NFYsU06zcoIcitNbE5FgqI3q_fXmqIF1QE5lOOqyCrahizIUWRs6PTZUbEnwrkKAF92AnJ5ACS22dvx5SVYSp5Rw39kYCortBH0182Av2KU2W0scyvWlkI1-rRkP8zTxUqfyOm0VpNSAsPcs3QQWzqCYWYQ3LW06V91oUIovifrnEKwcjWefs_N8RTpUvz56yUv2kwK4D1MK9MJE-e9oyAURF3vIuHg96vzN5FT6Bg6JcEa_hELUqOwNzM-euWI1zE7rP0-nM1p0RQSDVmZZ5jASfO1lfLvBwEtU3bJo4_sb9BZ5J3Lb1oX_i4X9evTJBoopm0BLKIMQN_ui_Znd9ohtgBryL0Z2hM1RH0xC4tiLlmKbY7jr9LJ_okJQFPtzI2XrVDHKf0XzPExBziEEc8kPuGtWpcSCpt-dXSQ5tqaAf5jcs=w988-h741-no") },
                new Item { Title = "Sunglasses", OwnerEmail="elijahtbouchard@gmail.com", OwnerNickname = "Elijah", Description = "I wear normal glasses so I have no need for these. No scratches.", CurrentHolderNickname = "Elijah", PhotoUrl = new Uri("https://cdn.shopify.com/s/files/1/0898/5824/products/QUAY_HIGHKEY_BLACK_FADE_FRONT_1024x1024.jpg?v=1537995892") } } };
        }


        public int Credits = 2;
        public List<Item> BrowsePage = new List<Item>();
        public List<ItemRequest> RequestedItems = new List<ItemRequest>();
        public List<Item> SubmittedItems = new List<Item>();


        public static void RequestItem(Item item)
        {
            Instance.BrowsePage.Remove(item);
            Instance.RequestedItems.Add(new ItemRequest { Item = item, CurrentHolderNickname = item.CurrentHolderNickname, OwnerNickname = item.CurrentHolderNickname, RequestDate = DateTime.Now });
        }

        public static void ClaimItem()
        {
            //TODO: Add this functionality
        }

        public static void AddItem(Item item)
        {
            Instance.Credits += 1;
            Instance.BrowsePage.Add(item);
            Instance.SubmittedItems.Add(item);
        }

        public static void CancelRequest(ItemRequest item)
        {
            Instance.BrowsePage.Add(item.Item);
            Instance.RequestedItems.Remove(item);
        }

    }

}
