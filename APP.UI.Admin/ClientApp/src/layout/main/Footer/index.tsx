import {defineComponent, ref} from 'vue';

import {Layout} from 'ant-design-vue';
import styles from './index.less';

const {Footer: ALayoutFooter} = Layout

export default defineComponent({
    name: 'PageFooter',
    components: {ALayoutFooter},
    setup() {
        return () => (
            <>
                <a-layout-footer class={styles.page_footer}>
                    版权所有 © 2016 <a href="http://www.7cit.com.cn/" target="_blank">广东七洲科技股份有限公司</a> 粤ICP备09159937号
                </a-layout-footer>
            </>
        );
    },
});
