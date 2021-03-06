USE [PodNoms]
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'29c0716a-94bc-4b79-bb7a-1acb2d872101', CAST(N'2018-08-09T21:46:46.8966667' AS DateTime2), CAST(N'2018-08-09T21:46:46.8966667' AS DateTime2), N'Comedy')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'db829bfe-a8fe-458e-9b67-2b00f4794750', CAST(N'2018-08-09T21:46:47.2866667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2866667' AS DateTime2), N'Technology')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'50495352-2339-4498-aad3-3f8c85f6ac69', CAST(N'2018-08-09T21:46:47.1933333' AS DateTime2), CAST(N'2018-08-09T21:46:47.1933333' AS DateTime2), N'Science & Medicine')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'f0603194-6f45-4695-98c1-6288cffbfd94', CAST(N'2018-08-09T21:46:46.8200000' AS DateTime2), CAST(N'2018-08-09T21:46:46.8200000' AS DateTime2), N'Business')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'67d057a7-21b4-4462-a284-66ba62a6de1b', CAST(N'2018-08-09T21:46:47.1466667' AS DateTime2), CAST(N'2018-08-09T21:46:47.1466667' AS DateTime2), N'Religion & Spirituality')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'3219621b-8311-4b65-bb48-6f68fba4957c', CAST(N'2018-08-09T21:46:47.1300000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1300000' AS DateTime2), N'Kids & Family')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'a6aa8e20-8729-4698-a254-976012afdbf3', CAST(N'2018-08-09T21:46:47.3333333' AS DateTime2), CAST(N'2018-08-09T21:46:47.3333333' AS DateTime2), N'TV & Film')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'97735523-d87a-4b5f-9dd1-ab8289af2ae6', CAST(N'2018-08-09T21:46:47.2400000' AS DateTime2), CAST(N'2018-08-09T21:46:47.2400000' AS DateTime2), N'Sports & Recreation')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'f177d65b-5eca-4137-b202-af672cd11d70', CAST(N'2018-08-09T21:46:47.2100000' AS DateTime2), CAST(N'2018-08-09T21:46:47.2100000' AS DateTime2), N'Society & Culture')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'27fb7005-b75c-490b-ae13-bcc88525be65', CAST(N'2018-08-09T21:46:47.1300000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1300000' AS DateTime2), N'Music')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'c4911d87-2b6e-42ea-b771-be910cb01624', CAST(N'2018-08-09T21:46:47.1000000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1000000' AS DateTime2), N'Health')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'5e023a7a-461d-46c6-bca8-c9049f6d2ec5', CAST(N'2018-08-09T21:46:47.1300000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1300000' AS DateTime2), N'News & Politics')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'b13cddb1-feff-42e1-9c80-cb8ad4a5f374', CAST(N'2018-08-09T21:46:47.0066667' AS DateTime2), CAST(N'2018-08-09T21:46:47.0066667' AS DateTime2), N'Games & Hobbies')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'ad31686b-794b-4ebb-99a8-cdc812ca7e83', CAST(N'2018-08-09T21:46:46.9133333' AS DateTime2), CAST(N'2018-08-09T21:46:46.9133333' AS DateTime2), N'Education')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', CAST(N'2018-08-09T21:46:46.5700000' AS DateTime2), CAST(N'2018-08-09T21:46:46.5700000' AS DateTime2), N'Arts')
INSERT [dbo].[Categories]
    ([Id], [CreateDate], [UpdateDate], [Description])
VALUES
    (N'2e23f263-062a-43c3-9e27-fb7555fb8e76', CAST(N'2018-08-09T21:46:47.0700000' AS DateTime2), CAST(N'2018-08-09T21:46:47.0700000' AS DateTime2), N'Government & Organizations')

INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'c2b6d61f-9070-4757-91ea-01e5ce0b1187', CAST(N'2018-08-09T21:46:46.6000000' AS DateTime2), CAST(N'2018-08-09T21:46:46.6000000' AS DateTime2), N'Design', N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'eaada893-dbb7-4381-9d8a-04670de03450', CAST(N'2018-08-09T21:46:47.2266667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2266667' AS DateTime2), N'History', N'f177d65b-5eca-4137-b202-af672cd11d70', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'df1b7cf4-bb92-44a0-a5fe-10f7eff74b3d', CAST(N'2018-08-09T21:46:47.3033333' AS DateTime2), CAST(N'2018-08-09T21:46:47.3033333' AS DateTime2), N'Tech News', N'db829bfe-a8fe-458e-9b67-2b00f4794750', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'dc71140f-fa43-4c50-a2e6-118e6d17f8a7', CAST(N'2018-08-09T21:46:47.0833333' AS DateTime2), CAST(N'2018-08-09T21:46:47.0833333' AS DateTime2), N'Regional', N'2e23f263-062a-43c3-9e27-fb7555fb8e76', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'd49987af-e983-460b-aea3-1718723a5405', CAST(N'2018-08-09T21:46:47.1933333' AS DateTime2), CAST(N'2018-08-09T21:46:47.1933333' AS DateTime2), N'Medicine', N'50495352-2339-4498-aad3-3f8c85f6ac69', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'fd718eca-d598-4a6a-a97a-1c5be5cc02ea', CAST(N'2018-08-09T21:46:47.2566667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2566667' AS DateTime2), N'College & High School', N'97735523-d87a-4b5f-9dd1-ab8289af2ae6', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'f573ed09-1db3-426b-8776-263776b755e0', CAST(N'2018-08-09T21:46:47.0833333' AS DateTime2), CAST(N'2018-08-09T21:46:47.0833333' AS DateTime2), N'National', N'2e23f263-062a-43c3-9e27-fb7555fb8e76', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'781cafc3-411e-4c4f-be07-26bf503623be', CAST(N'2018-08-09T21:46:47.0066667' AS DateTime2), CAST(N'2018-08-09T21:46:47.0066667' AS DateTime2), N'Automotive', N'b13cddb1-feff-42e1-9c80-cb8ad4a5f374', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'd2e1f242-fdb3-4075-8336-27344912ed6d', CAST(N'2018-08-09T21:46:47.3033333' AS DateTime2), CAST(N'2018-08-09T21:46:47.3033333' AS DateTime2), N'Gadgets', N'db829bfe-a8fe-458e-9b67-2b00f4794750', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'aae0e462-4879-452f-b0e1-2bae75f2b7da', CAST(N'2018-08-09T21:46:47.1000000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1000000' AS DateTime2), N'Alternative Health', N'c4911d87-2b6e-42ea-b771-be910cb01624', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'5ed9373c-0092-48af-9d7d-32a314239157', CAST(N'2018-08-09T21:46:47.2100000' AS DateTime2), CAST(N'2018-08-09T21:46:47.2100000' AS DateTime2), N'Natural Sciences', N'50495352-2339-4498-aad3-3f8c85f6ac69', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'1f4b0b76-d7b8-404c-af32-3428fc488e30', CAST(N'2018-08-09T21:46:47.0533333' AS DateTime2), CAST(N'2018-08-09T21:46:47.0533333' AS DateTime2), N'Video Games', N'b13cddb1-feff-42e1-9c80-cb8ad4a5f374', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'84ed0b38-616d-4926-90c2-3cc7ae2f8e4e', CAST(N'2018-08-09T21:46:47.2866667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2866667' AS DateTime2), N'Professional', N'97735523-d87a-4b5f-9dd1-ab8289af2ae6', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'867ae49c-ade1-41b9-ba18-3f4801ec18ee', CAST(N'2018-08-09T21:46:47.1166667' AS DateTime2), CAST(N'2018-08-09T21:46:47.1166667' AS DateTime2), N'Fitness & Nutrition', N'c4911d87-2b6e-42ea-b771-be910cb01624', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'b4284990-c542-48e4-a000-42d27202153b', CAST(N'2018-08-09T21:46:47.0833333' AS DateTime2), CAST(N'2018-08-09T21:46:47.0833333' AS DateTime2), N'Non-Profit', N'2e23f263-062a-43c3-9e27-fb7555fb8e76', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'c950eaac-f164-4f3c-8384-459679130aef', CAST(N'2018-08-09T21:46:47.2566667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2566667' AS DateTime2), N'Outdoor', N'97735523-d87a-4b5f-9dd1-ab8289af2ae6', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'df0063a6-31ef-4f9a-be23-4f9178291bb3', CAST(N'2018-08-09T21:46:46.9300000' AS DateTime2), CAST(N'2018-08-09T21:46:46.9300000' AS DateTime2), N'Higher Education', N'ad31686b-794b-4ebb-99a8-cdc812ca7e83', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'2488b878-124a-42f9-a761-53daa1eefde1', CAST(N'2018-08-09T21:46:46.6633333' AS DateTime2), CAST(N'2018-08-09T21:46:46.6633333' AS DateTime2), N'Literature', N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'5c40fa5a-72df-475b-bfa8-561955145762', CAST(N'2018-08-09T21:46:47.1800000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1800000' AS DateTime2), N'Other', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'201fb42b-e241-4e16-bea5-5df768064402', CAST(N'2018-08-09T21:46:46.8666667' AS DateTime2), CAST(N'2018-08-09T21:46:46.8666667' AS DateTime2), N'Investing', N'f0603194-6f45-4695-98c1-6288cffbfd94', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'4f006295-e8ea-4a87-92e0-69e32f8d3ad9', CAST(N'2018-08-09T21:46:47.0066667' AS DateTime2), CAST(N'2018-08-09T21:46:47.0066667' AS DateTime2), N'Aviation', N'b13cddb1-feff-42e1-9c80-cb8ad4a5f374', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'8b2a590b-bb59-4f30-acb2-72fa01985e4b', CAST(N'2018-08-09T21:46:47.2100000' AS DateTime2), CAST(N'2018-08-09T21:46:47.2100000' AS DateTime2), N'Social Sciences', N'50495352-2339-4498-aad3-3f8c85f6ac69', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'3e3f77a4-4cd3-4644-ace1-7923e84d403d', CAST(N'2018-08-09T21:46:46.6933333' AS DateTime2), CAST(N'2018-08-09T21:46:46.6933333' AS DateTime2), N'Performing Arts', N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'f9167edb-5d9c-4e34-ad26-7a2630528682', CAST(N'2018-08-09T21:46:47.2400000' AS DateTime2), CAST(N'2018-08-09T21:46:47.2400000' AS DateTime2), N'Places & Travel', N'f177d65b-5eca-4137-b202-af672cd11d70', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'4ecd4db0-0786-4594-a49b-86ab1362bc3c', CAST(N'2018-08-09T21:46:47.2566667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2566667' AS DateTime2), N'Amateur', N'97735523-d87a-4b5f-9dd1-ab8289af2ae6', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'375fa60d-8d6f-4684-a729-8a061ce2e062', CAST(N'2018-08-09T21:46:46.8666667' AS DateTime2), CAST(N'2018-08-09T21:46:46.8666667' AS DateTime2), N'Management & Marketing', N'f0603194-6f45-4695-98c1-6288cffbfd94', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'5005a13d-45c9-40ea-b691-96ff5afa0e39', CAST(N'2018-08-09T21:46:47.2266667' AS DateTime2), CAST(N'2018-08-09T21:46:47.2266667' AS DateTime2), N'Personal Journals', N'f177d65b-5eca-4137-b202-af672cd11d70', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'd9d8f925-cf6f-4d8d-9404-990609b312ce', CAST(N'2018-08-09T21:46:47.1466667' AS DateTime2), CAST(N'2018-08-09T21:46:47.1466667' AS DateTime2), N'Buddhism', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'd5e38344-c701-406f-8762-9a793efb98d7', CAST(N'2018-08-09T21:46:47.1633333' AS DateTime2), CAST(N'2018-08-09T21:46:47.1633333' AS DateTime2), N'Christianity', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'bd367623-40c3-48f2-9a59-9b526c643905', CAST(N'2018-08-09T21:46:47.1633333' AS DateTime2), CAST(N'2018-08-09T21:46:47.1633333' AS DateTime2), N'Hinduism', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'797408d2-abbc-4d8d-92f4-9b74146d726d', CAST(N'2018-08-09T21:46:46.8200000' AS DateTime2), CAST(N'2018-08-09T21:46:46.8200000' AS DateTime2), N'Business News', N'f0603194-6f45-4695-98c1-6288cffbfd94', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'7174aa7c-8df2-4f9f-84aa-9bc1e9793eda', CAST(N'2018-08-09T21:46:47.0366667' AS DateTime2), CAST(N'2018-08-09T21:46:47.0366667' AS DateTime2), N'Other Games', N'b13cddb1-feff-42e1-9c80-cb8ad4a5f374', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'd69e2cc2-588f-49c5-933e-a6f963e79f32', CAST(N'2018-08-09T21:46:47.0700000' AS DateTime2), CAST(N'2018-08-09T21:46:47.0700000' AS DateTime2), N'Local', N'2e23f263-062a-43c3-9e27-fb7555fb8e76', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'c8829517-5a62-4bcc-82cc-ab4a75c6312c', CAST(N'2018-08-09T21:46:46.9433333' AS DateTime2), CAST(N'2018-08-09T21:46:46.9433333' AS DateTime2), N'K-12', N'ad31686b-794b-4ebb-99a8-cdc812ca7e83', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'7be75c87-d18a-45ee-92c3-ab6afce3e5db', CAST(N'2018-08-09T21:46:46.6300000' AS DateTime2), CAST(N'2018-08-09T21:46:46.6300000' AS DateTime2), N'Food', N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'1d5212bc-b31c-4130-bcea-ae7c9df2985f', CAST(N'2018-08-09T21:46:47.2400000' AS DateTime2), CAST(N'2018-08-09T21:46:47.2400000' AS DateTime2), N'Philosophy', N'f177d65b-5eca-4137-b202-af672cd11d70', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'4966ec0c-3f53-4710-9f21-afb2371ab3e2', CAST(N'2018-08-09T21:46:47.1166667' AS DateTime2), CAST(N'2018-08-09T21:46:47.1166667' AS DateTime2), N'Sexuality', N'c4911d87-2b6e-42ea-b771-be910cb01624', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'd8c5c265-1fa1-4244-9d76-b181bd936846', CAST(N'2018-08-09T21:46:46.9133333' AS DateTime2), CAST(N'2018-08-09T21:46:46.9133333' AS DateTime2), N'Education Technology', N'ad31686b-794b-4ebb-99a8-cdc812ca7e83', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'7e371ab3-cc53-4f16-9de9-c2d10faa8938', CAST(N'2018-08-09T21:46:46.8033333' AS DateTime2), CAST(N'2018-08-09T21:46:46.8033333' AS DateTime2), N'Visual Arts', N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'c546f84f-643a-4f44-9bdf-ce4b44926e0a', CAST(N'2018-08-09T21:46:46.9766667' AS DateTime2), CAST(N'2018-08-09T21:46:46.9766667' AS DateTime2), N'Training', N'ad31686b-794b-4ebb-99a8-cdc812ca7e83', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'149f1619-eb42-4d9b-a4d6-da68ec0541d9', CAST(N'2018-08-09T21:46:47.3200000' AS DateTime2), CAST(N'2018-08-09T21:46:47.3200000' AS DateTime2), N'Podcasting', N'db829bfe-a8fe-458e-9b67-2b00f4794750', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'37f4f907-7b02-4268-8e38-db8700c1dc87', CAST(N'2018-08-09T21:46:47.1800000' AS DateTime2), CAST(N'2018-08-09T21:46:47.1800000' AS DateTime2), N'Judaism', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'8114a581-24bb-451e-ba21-dcacd175bde4', CAST(N'2018-08-09T21:46:46.9433333' AS DateTime2), CAST(N'2018-08-09T21:46:46.9433333' AS DateTime2), N'Language Courses', N'ad31686b-794b-4ebb-99a8-cdc812ca7e83', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'35cf95e2-6506-47ad-bd5d-df3b5e389a8f', CAST(N'2018-08-09T21:46:46.6000000' AS DateTime2), CAST(N'2018-08-09T21:46:46.6000000' AS DateTime2), N'Fashion & Beauty', N'41b9ee87-a9ca-4305-8ed8-ee69a3dbcfc3', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'8fb81212-5911-4190-9635-e478e2119c0c', CAST(N'2018-08-09T21:46:47.1933333' AS DateTime2), CAST(N'2018-08-09T21:46:47.1933333' AS DateTime2), N'Spirituality', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'23eb4b47-fc1d-45ac-9a21-e4ea5313f3d8', CAST(N'2018-08-09T21:46:47.0366667' AS DateTime2), CAST(N'2018-08-09T21:46:47.0366667' AS DateTime2), N'Hobbies', N'b13cddb1-feff-42e1-9c80-cb8ad4a5f374', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'c7465257-d5b6-46f3-8521-e7a8f91e17b8', CAST(N'2018-08-09T21:46:46.8966667' AS DateTime2), CAST(N'2018-08-09T21:46:46.8966667' AS DateTime2), N'Shopping', N'f0603194-6f45-4695-98c1-6288cffbfd94', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'cbcf3c5f-f7c9-44b3-9b2d-edc6523d8c3c', CAST(N'2018-08-09T21:46:47.1166667' AS DateTime2), CAST(N'2018-08-09T21:46:47.1166667' AS DateTime2), N'Self-Help', N'c4911d87-2b6e-42ea-b771-be910cb01624', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'fb7c2755-4b09-4d9f-a457-eebb39abac3b', CAST(N'2018-08-09T21:46:46.8500000' AS DateTime2), CAST(N'2018-08-09T21:46:46.8500000' AS DateTime2), N'Careers', N'f0603194-6f45-4695-98c1-6288cffbfd94', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'eb082ac8-856b-4bb0-a22f-f4062d4347d2', CAST(N'2018-08-09T21:46:47.3200000' AS DateTime2), CAST(N'2018-08-09T21:46:47.3200000' AS DateTime2), N'Software How-To', N'db829bfe-a8fe-458e-9b67-2b00f4794750', NULL)
INSERT [dbo].[Subcategories]
    ([Id], [CreateDate], [UpdateDate], [Description], [CategoryId], [PodcastId])
VALUES
    (N'56a5dc3b-3dc5-46d2-9972-f846a4ca9909', CAST(N'2018-08-09T21:46:47.1633333' AS DateTime2), CAST(N'2018-08-09T21:46:47.1633333' AS DateTime2), N'Islam', N'67d057a7-21b4-4462-a284-66ba62a6de1b', NULL)
