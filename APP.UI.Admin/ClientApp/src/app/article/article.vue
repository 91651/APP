<template>
  <Card>
      <Table :columns="[
                    {
                        title: '序号',
                        key: 'id'
                    },
                    {
                        title: '标题',
                        key: 'title'
                    },
                    {
                        title: '创建时间',
                        key: 'created',
                        render: (h,params)=>{
                            return h('div',
                                $moment(params.row.created).fromNow()
                            )
                        }
                    }
                ]" :data="articles"></Table>
                <Page :total="100" show-sizer @on-change="pageChange" @on-page-size-change="pageSizeChange" />
    </Card>
</template>

<script lang="ts" src="./article.ts"></script>